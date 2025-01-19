# Boundary Definer 

Boundary Definer is a product developed for the IMT&S project as part of the fourth-year course - Creative Media and Game Technologies - at Saxion University of Applied Sciences.

As the main engineer for this project, I was responsible for writing most of the code. The only parts I did not contribute to were related to the networking functionality.

Beyond programming, I also took on the role of a technical designer with a specific focus: creating tools to streamline the work of our artists and designers throughout the project.

<p align="center">
  <img width="460" height="300" src="https://github.com/kamuuucka/BorderCrossingCMGT/blob/readmeUpdates/ReadmeFiles/GraphWork.gif">
</p>

My primary focus was developing a tool for creating and customizing graphs, which serve as a central component of our app. I aimed to make the tool intuitive and highly adjustable, allowing designers to create and modify graphs without needing to edit the code. This approach ensured that team members could independently adjust the graph as needed, enabling smoother collaboration and reducing reliance on my availability for technical guidance.

<img align="left" src="https://github.com/kamuuucka/BorderCrossingCMGT/blob/readmeUpdates/ReadmeFiles/graphGenerator.png">

The tool consists of two main scripts: one responsible for drawing the graph and another for managing its functionalities. I made sure that all necessary fields are exposed and easily modifiable through the Unity Inspector, making it simple for designers to work with.

Creating the graph is intuitive — designers only need to specify parameters such as the line width and colors, which also define the number of layers. The graph generator script provides clear feedback by informing users about any additional required objects that need to be assigned for successful graph generation. To further simplify usage, all exposed fields include tooltips to guide the user.

![](https://github.com/kamuuucka/BorderCrossingCMGT/blob/readmeUpdates/ReadmeFiles/tooltip.png)

![](https://github.com/kamuuucka/BorderCrossingCMGT/blob/readmeUpdates/ReadmeFiles/tooltip1.png)

The second script, the manager, handles the manipulation of the graph and saving its data. It utilizes events to manage interactions during the graph's rotation and triggers specific actions when the graph completes a full cycle. This event-driven approach ensures efficient control and responsiveness, improving the overall functionality of the tool.

Additionally I wrote a whole bunch of scripts that can be used in various places in the project. All of those are in the Scripts -> Tools

