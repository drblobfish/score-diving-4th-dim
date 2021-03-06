# Software for the part of the experiment on computer

This software first present a main menu.

From the main menu, one can access a tutorial screen where participants learn how to play and pause the animation of 3d model of a cat and drag and drop items from green platforms to red platforms in order to sort them.

From main menu, one can also setup the ID of the participant and select the dataset to display.

When a participant click on "Start Experiment" the selected dataset is diplayed to them in the sequence screen. They can rotate, zoom and pause the animation.
The animation of the dataset is showed 3 times, spaced by a 5 second delay.
At the end of the 3 sequences, the participant goes to the sorting screen.

In the sorting screen, 14 global shapes are shown to the participant in the upper part of the screen. 7 sorting platforms are in the lower part of the screen.
Participants can drag and drop items, make the items return to their initial position and focus on a specific item.
When an item is focused, it is displayed in the middle of the screen, and participants can rotate it and zoom.
Once the items are sorted, a button appears proposing participants to validate their choices.

## Controls

**Sequence screen**

- space bar : play/pause

- left click and drag : rotate dataset

- scroll : zoom

- right click and drag : paning camera

**Sorting Screen**

- drag and drop : move answers

- right click on answer : move answer to its initial position

- double click on answer : focus on answer (in focus mode the controls are the same as in sequence screen)

## files

```
.
├── Assets
│   └── dataset                # 3d files for the 1st dataset
│   │   └── false              # 3d files for the false answers of the 1st dataset
│   │   │   └── false1.obj
│   │   │   └── false2.obj
│   │   │   ...
│   │   └── true               # 3d files for the true answers of the 1st dataset
│   │   │   └── true1.obj
│   │   │   └── true2.obj
│   │   │   ...
│   │   └── cell_dataset.dae   # The dataset in collada format
│   └── dataset_2              # Contains 3d files for the 1st dataset (same structure as dataset)
│   └── Scripts                # Scripts
│   │   └── Dataset Manager    # Script for the sequence screen
│   │   └── Sorting Manager    # Script for the sorting screen
│   │   └── Tutorial           # Script for the tutorial
│   └── Scene
│   │   └── Interface
│   │   └── SortingScene  
│   │   └── Tutorial  
...
└── README.md
```

```mermaid
classDiagram
class SequenceManager{
    <<SequenceManagerEmpty>>
    timesPlayed
    BeginSequences()
    PlaySequence()
    EndAnimation()
    StartTimer()
    EndTimer()
}
class DatasetAnim{
    <<Dataset>>
    speed
    StartAnim()
    Pause()
    Play()
    EndAnimation()
}
class OnEndAnimation{
    <<DatasetMesh>>
    EndAnimation()
}

class UI{
    TimerUI
    SequenceUI
}

OnEndAnimation-->DatasetAnim : catch animation end
DatasetAnim --> SequenceManager : raise animation end
DatasetAnim <-- SequenceManager : call
UI <-- SequenceManager : control
```
