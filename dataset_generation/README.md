# Dataset Generation

This blend file allows to automate the generation of datasets similar to the one used in the study.

## Workflow

1. Open `boids_in_morphing_shape.blend`

2. Change the parameters of the boid simulation by clicking on the object `particle_src` and going in the particle system tab

3. Change the variables in the scripts to your liking (cf parameters).

4. Suppress if needed the files created by your previous attempts at generating the datasets

5. Run the script (takes about 12 minutes per dataset with the base parameters)

## Files created

Running the blender script will create folders named `0`, `1`, ... to the number of dataset you want to create. Each of them will contain the dataset under the name `cell_dataset<number of the dataset>.dae` it's a 3d mesh + animation file in the collada format that contains the dataset : all the individual cells moving in boidian movement. The folder will also contain another folder named `true` that contains all the snapshot of the global shape of the dataset as `true<number of the snapshot>.obj` as well as `.mtl` files (useless unless you want to create materials inside blender)

## Parameters

- `nbDataset : int = 10` : number of dataset to generate
- `nbFreezeFrame : int = 7` : number of snapshot of the global shape of the dataset to take for each dataset
- `nbParticle : int = 1000` : number of particle to simulate in each dataset
- `pathTextureCoordBasePos : Vector3 = (0,0,0)` : you can think about this vector as the seed of the simulation, change it if you want different datasets than ours
- `KEYFRAME_ROTATION : bool = true` : wether you want to record the rotation of the particles