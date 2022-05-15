# Data analysis

These folders contain the python notebook used for the analysis, our raw and cleaned data in csv files as well as svg files of the plot used in our poster

## Files

```
.
├── clean-data           # tidy data with correct types, added missing values and removed problematic values
│   └── experiments.csv
│   └── focusTime.csv    # time each participant spent in focus mode on each shape on computer
│   └── participants.csv # answers to the 2 surveys
│   └── slots.csv        # the slot/shape pairs participants did in the sorting screen
├── computer data        # scores we computed based on our data
├── plot                 # raw plots used for the poster in svg format 
└── raw                  # data as we obtained it from our softwares and google form
    └── forms            # csv of the survey answers as obtained from google form
    └── json             # each json correspond to an experiment
```

## Dependencies

- `pandas`
- `numpy`
- `seaborn`
- `matplotlib`
- `scipy.stats`
- `os`
- `json`
- `glob`
- `datetime`