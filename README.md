# CrossPlatformPractice

## Overview

The repository is a collection of applications created for the practice of creating cross-platform applications using Microsoft MAUI (Multi-platform App UI).

*All apps work on both Windows and Android*

## Mealy State Machine Vizualizer

![GraphDemo128](https://user-images.githubusercontent.com/61901199/210269198-3497a940-1fbc-49b7-932a-5e881fe845b2.gif)


Converts json to graph visualization with support for several types of joins: direct, bidirectional, and ring.

JSON example:
```json
[
    {
        "NodeText": "a1",
        "ConnectedNodes": {
            "a2": "z2/w1"
        }
    },
    {
        "NodeText": "a2",
        "ConnectedNodes": {
            "a3": "z1/w2"
        }
    },
    {
        "NodeText": "a3",
        "ConnectedNodes": {
            "a1": "z1/w2"
        }
    }
]
```

## Pocket Users Database

![PocketDBDemo](https://user-images.githubusercontent.com/61901199/210271371-0519bf36-cfa5-4eea-903e-b1d378de71bf.png)

In the application, you can create, modify, delete contact entries. You can set a username, email and phone number for entries. You can also save and load databases.
