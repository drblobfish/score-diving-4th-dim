#========================Imports==========================

import bpy
import mathutils as mtu
import os

#=========================Params============================

nbDataset = 2

nbFrame = 50
nbFreezeFrame = 7

nbParticle = 100

pathTextureCoordBasePos = mtu.Vector((0,0,0))


#=========================Global========================

scene = bpy.data.scenes["Scene"]
particle_src = scene.objects["particle_src"]
particle_sys = particle_src.modifiers["ParticleSettings"]
particle_mesh = scene.objects["Icosphere"]

pathTextureCoord = scene.objects["path_texture_coord"]
pathTextureCoord.location = pathTextureCoordBasePos

timePoints = list(range(1,nbFrame,int(nbFrame/nbFreezeFrame)))


#=========================Freeze Frame========================



def setBaseParam():
    scene.frame_end=nbFrame
    particle_sys.particle_system.settings.count = nbParticle

def deleteTrue():
    bpy.ops.object.select_all(action='DESELECT')
    for object in bpy.data.collections['true'].objects:
        object.select_set(True)
    bpy.ops.object.delete(use_global=False)
    
def deleteParticle():
    bpy.ops.object.select_all(action='DESELECT')
    for object in bpy.data.collections['particles'].objects:
        object.select_set(True)
    bpy.ops.object.delete(use_global=False)
    

def freezeCurrentFrame(i) :
    # selecting the particle src
    bpy.ops.object.select_all(action='DESELECT')
    particle_src.select_set(True)
    bpy.context.view_layer.objects.active = particle_src

    bpy.ops.object.duplicate_move(
        OBJECT_OT_duplicate={"linked":False, "mode":'TRANSLATION'},
        TRANSFORM_OT_translate={
            "value":(0, 0, 0),
            "orient_type":'GLOBAL',
            "orient_matrix":((0, 0, 0), (0, 0, 0), (0, 0, 0)),
            "orient_matrix_type":'GLOBAL',
            "constraint_axis":(False, False, False),
            "mirror":False,
            "use_proportional_edit":False,
            "proportional_edit_falloff":'SMOOTH',
            "proportional_size":1,
            "use_proportional_connected":False,
            "use_proportional_projected":False,
            "snap":False, "snap_target":'CLOSEST',
            "snap_point":(0, 0, 0),
            "snap_align":False,
            "snap_normal":(0, 0, 0),
            "gpencil_strokes":False,
            "cursor_transform":False,
            "texture_space":False,
            "remove_on_cancel":False,
            "release_confirm":False,
            "use_accurate":False})

    dupliPartSrc = bpy.context.view_layer.objects.active

    bpy.ops.object.particle_system_remove()

    for dupliModifier in dupliPartSrc.modifiers:
        bpy.ops.object.modifier_apply(modifier=dupliModifier.name,apply_as='DATA')
    
    # move into true collection
    oldCollection = dupliPartSrc.users_collection
    bpy.data.collections["true"].objects.link(dupliPartSrc)
    for ob in oldCollection:
        ob.objects.unlink(dupliPartSrc)
    
    dupliPartSrc.name = "true" + str(i)
        

def freezAllFrame():
    for i,frame in enumerate(timePoints):
        bpy.context.scene.frame_set(frame)
        freezeCurrentFrame(i)

        
def batchExportTrue(dataset : str):
    # export to blend file location
    basedir = os.path.dirname(bpy.data.filepath)

    if not basedir:
        raise Exception("Blend file is not saved")

    view_layer = bpy.context.view_layer
    
    
    bpy.ops.object.select_all(action='DESELECT')
    
    for object in bpy.data.collections['true'].objects:
        object.select_set(True)

    selection = bpy.context.selected_objects

    bpy.ops.object.select_all(action='DESELECT')

    for obj in selection:

        obj.select_set(True)

        # some exporters only use the active object
        view_layer.objects.active = obj
        
        name = bpy.path.clean_name(obj.name)
        folder = os.path.join(basedir,dataset,"true")
        
        if not os.path.exists(folder):
            os.makedirs(folder)
        
        fn = os.path.join(folder,name)

        bpy.ops.export_scene.obj(filepath=fn + ".obj", use_selection=True)

        # Can be used for multiple formats
        # bpy.ops.export_scene.x3d(filepath=fn + ".x3d", use_selection=True)

        obj.select_set(False)

        print("written:", fn)


#===========================Bake Particles====================


def bakeParticles():
    bpy.ops.object.select_all(action='DESELECT')
    bpy.context.view_layer.objects.active = particle_src
    override = {'scene': scene, 'active_object': particle_src, 'point_cache': particle_sys.particle_system.point_cache}
    bpy.ops.ptcache.free_bake(override)
    bpy.ops.ptcache.bake(override, bake=True)
    
def freeBakeParticles():
    bpy.ops.object.select_all(action='DESELECT')
    bpy.context.view_layer.objects.active = particle_src
    override = {'scene': scene, 'active_object': particle_src, 'point_cache': particle_sys.particle_system.point_cache}
    bpy.ops.ptcache.free_bake(override)

#=========================Particle To Mesh=========================

# Set these to False if you don't want to key that property.
KEYFRAME_LOCATION = True
KEYFRAME_ROTATION = True
KEYFRAME_SCALE = True
KEYFRAME_VISIBILITY = False  # Viewport and render visibility.
KEYFRAME_VISIBILITY_SCALE = True


def create_objects_for_particles(ps, obj):
    # Duplicate the given object for every particle and return the duplicates.
    # Use instances instead of full copies.
    obj_list = []
    mesh = obj.data
    particles_coll = bpy.data.collections["particles"]

    for i, _ in enumerate(ps.particles):
        dupli = bpy.data.objects.new(
                    name="particle.{:03d}".format(i),
                    object_data=mesh)
        particles_coll.objects.link(dupli)
        obj_list.append(dupli)
    return obj_list

def match_and_keyframe_objects(ps, obj_list, start_frame, end_frame):
    # Match and keyframe the objects to the particles for every frame in the
    # given range.
    for frame in range(start_frame, end_frame + 1):
        print("frame {} processed".format(frame))
        bpy.context.scene.frame_set(frame)
        for p, obj in zip(ps.particles, obj_list):
            match_object_to_particle(p, obj)
            keyframe_obj(obj)

def match_object_to_particle(p, obj):
    # Match the location, rotation, scale and visibility of the object to
    # the particle.
    loc = p.location
    rot = p.rotation
    size = p.size
    if p.alive_state == 'ALIVE':
        vis = True
    else:
        vis = False
    obj.location = loc
    # Set rotation mode to quaternion to match particle rotation.
    obj.rotation_mode = 'QUATERNION'
    obj.rotation_quaternion = rot
    if KEYFRAME_VISIBILITY_SCALE:
        if vis:
            obj.scale = (size, size, size)
        if not vis:
            obj.scale = (0.001, 0.001, 0.001)
    obj.hide_viewport = not(vis) # <<<-- this was called "hide" in <= 2.79
    obj.hide_render = not(vis)

def keyframe_obj(obj):
    # Keyframe location, rotation, scale and visibility if specified.
    if KEYFRAME_LOCATION:
        obj.keyframe_insert("location")
    if KEYFRAME_ROTATION:
        obj.keyframe_insert("rotation_quaternion")
    if KEYFRAME_SCALE:
        obj.keyframe_insert("scale")
    if KEYFRAME_VISIBILITY:
        obj.keyframe_insert("hide_viewport") # <<<-- this was called "hide" in <= 2.79
        obj.keyframe_insert("hide_render")


def particleToMesh():
    #in 2.8 you need to evaluate the Dependency graph in order to get data from animation, modifiers, etc
    depsgraph = bpy.context.evaluated_depsgraph_get()

    # Assume only 2 objects are selected.
    # The active object should be the one with the particle system.
    ps_obj = particle_src
    ps_obj_evaluated = depsgraph.objects[ ps_obj.name ]
    obj = particle_mesh

    for psy in ps_obj_evaluated.particle_systems:         
        ps = psy  # Assume only 1 particle system is present.
        start_frame = bpy.context.scene.frame_start
        end_frame = bpy.context.scene.frame_end
        obj_list = create_objects_for_particles(ps, obj)
        match_and_keyframe_objects(ps, obj_list, start_frame, end_frame)


#========================Export Mesh Particles=============================

def exportMeshParticlesToCollada(dataset):
    
    basedir = os.path.dirname(bpy.data.filepath)

    if not basedir:
        raise Exception("Blend file is not saved")
        
    name = "cell_dataset" + dataset
    folder = os.path.join(basedir,dataset)
    
    if not os.path.exists(folder):
        os.makedirs(folder)
        
    fn = os.path.join(folder,name)
    
    bpy.ops.object.select_all(action='DESELECT')
    
    for object in bpy.data.collections['particles'].objects:
        object.select_set(True)
    
    print(f"Exporting to collada dataset {dataset} in {folder}")
    bpy.ops.wm.collada_export(filepath=fn + ".dae", selected=True)
    print("Exporting to collada finished")

#==============================Main======================================

def main():
    
    setBaseParam()
    for i in range(nbDataset):
        
        print(f"Dealing with dataset {i}")
        
        deleteTrue()
        print(f"true deleted. {i}/{nbDataset}")
        
        freezAllFrame()
        print(f"frame freezed. {i}/{nbDataset}")
        
        batchExportTrue(str(i))
        print(f"exported true to obj. {i}/{nbDataset}")
        

        deleteParticle()
        print(f"particles deleted. {i}/{nbDataset}")
        
        bakeParticles()
        
        particleToMesh()
        print(f"particles converted to meshes. {i}/{nbDataset}")
        
        freeBakeParticles()
        
        exportMeshParticlesToCollada(str(i))
        print(f"mesh particles exported to dae. {i}/{nbDataset}")
        
        pathTextureCoord.location += mtu.Vector((10,0,0))
    
main()