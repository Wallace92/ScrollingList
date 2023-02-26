# Scrolling List with Dynamic PNG Images

This project is a Unity-based application that allows you to display a scrolling list of items with appropriate PNG images, 
names, and the time since their creation. In addition, this project also allows you to add another 
PNG file during runtime and refresh the content of the scrolling list. A progress bar is also included to inform you about the loading progress.

## Features
Displays a scrolling list of items with appropriate PNG images, names, and the time since their creation.
Allows adding another PNG file during runtime.
Refreshes the content of the scrolling list.
Includes a progress bar that informs you about the loading progress.
Supports two approaches: database and Addressables.
Both approaches are async type.

## Technologies
Unity 2022.2.0f1  
C#  
Addressables  

## Installation

Clone the repository to your local machine
```
git clone https://github.com/your_username/scrolling-list.git
```

## Usage
1. In the Unity Editor, go to the "Hierarchy" window and select the "ScrollableListManager" object.
2. Choose path with your content
3. When using addressables provide the label name and groupName
4. Select loading type by clicking the bolean value.

## Contributing
Contributions are always welcome! If you would like to contribute to this project, please fork the repository and submit a pull request.

## Credits
This project was created by Mateusz Glinkowski (mateuszwallace@gmail.com).
