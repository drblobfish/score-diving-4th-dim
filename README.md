# score-diving-4th-dim

Thanks to keener microscopy techniques, microscopy data are of increasingly high complexity. Analysis of this data by humans requires sharpened visualization tools. To this end Virtual Reality (VR) could be a tool of interest, as it immerses the user in a virtual environment along with its data. We conducted an experiment comparing the ability of a user to memorize and recall 3D objects using VR or traditional desktop visualizers we developed. These 3D objects correspond to the shape encapsulating small moving particles. This process yielded no significant difference between the user’s results and the null distribution. Nonetheless, we observed a significant progression in VR not observed when using the desktop version of our software.

This repository contains the code of the custom softwares used for the experiments along with the code used for data analysis and our documentation.

## Links

Our poster can be found [in pdf format here](https://drblobfish.github.io/assets/pdf/score/poster.pdf).

Our bibliography is available in [this zotero library](https://www.zotero.org/groups/4625494/score-4d-microscopy/library).

More information can be found on our [CRI project page](https://projects.cri-paris.org/projects/rRiHhgY3/summary).

For reference, our initial protocol (as of january 2022) is available [here](https://docs.google.com/document/d/1nIPCcI1o5f8sB7eeenndRpnXQNSKvKGWUfrj1_Wj8oY/edit?usp=sharing).

## Files

```
.
├── dataset_generation           # Blender files and python script to generate the datasets
├── documentation                # Poster, images, article
├── Mail Sending                 # Python script to send automatic mail to participants
├── Unity                        # unity projects
│   └── 4th dimension VR version # unity code for the software to do the experiment in VR
│   └── My Project               # unity code for the software to do the experiment on a 2d screen
└── README.md
```

## Context

This code is produced as a "SCORE" student project in the [Frontiers of Life Bachelor](https://licence.learningplanetinstitute.org/fr) (Center for Interdisciplinary Research, University of Paris) unders supervision by fellow Léo Blondel.
