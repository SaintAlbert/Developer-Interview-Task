# Developer-Interview-Task


## Task
We would like you to demonstrate your development skills by delivering a single page web site based on a set of requirements that match the way we deliver solutions within our team.

* We don't expect that this task should take longer than a few hours.  However, the amount of time you spend is entirely up to you
* You will need a **GitHub account** to clone this repository (Developer-Interview-Task)
* Once you have finished, please push it to your own GitHub repository and let us know

=======

During the face-to-face interview we would like to discuss your solution and your approach. We will expect you to demonstrate how the acceptance criteria have been met and also any challenges you faced during development, together with any enhancements that you'd make if you had further time.

If you have any questions, please get in touch.

*Good luck :)*

## Background
The Marie Curie Caring Services department wish to better promote their Helper service on the charity's website.

The Helper Service matches trained volunteers with people living with a terminal illness and provides support to them or their families. Support is provided free of charge and is available to people aged 18 or over with any terminal illness, and their families

The Website Scrum team Product Owner and Business Analyst have agreed with the Caring Services team that a page will be developed for the website that will list out available Helper Services; show how they can be contacted and dynamically highlight if and when they can be contacted.

## User Story
The following user story and acceptance criteria was created and will be used by the team to design and validate their work.

**See image below for an idea of how the page will look - the template has been provided for you**

*"As a website user who needs help and support
I want to be able to find Helper Services
so that I know what support is available to me and how to contact them"*

**Background: GIVEN I am on the Marie Curie website**
 
**WHEN I view the Helper Service page**

**AC1: Display**

THEN I want to see a list that contains all Marie Curie Helper Services

**AC2: Detail**

THEN each Helper Service list item will have the following properties: Title - linked to a page (https://www.mariecurie.org.uk/help/helper-volunteers), Description, Telephone number and a currently open / closed status

**AC3: Dynamic status**

THEN I want to see whether the helpline is OPEN or CLOSED with the label copy as per below

OPEN - OPEN TODAY UNTIL `{closing time}`

CLOSED - REOPENS `{day}` at `{opening time}`

**AC4: Visual** 

THEN the Helper Service list item displays a clear visual representation of whether it is the service is open or closed

Use grey background on card for closed, and orange for open

The style sheet classes that you will need to use to indicate the closed and open states are –

bg-color-light-grey and
bg-color-donation-orange

**AC5: Backend**

Create a simple logger service (from scratch, to a file) that can capture all requests made to the service (maybe write out time of access, IP address - anything you'd like), plus log any errors that the application generates.

**AC6: Backend**

We'd like the current weather for each of the helper regions under their name - rough location is fine.

Create a button, that when pressed calls an action on a controller that sends a request to a weather API https://openweathermap.org/current (full documentation on that page) and display the response on the page.

You will need to registar to gain access to an API key which has free usage - we recommend you do this earlier, as there may sometimes be delay in key activation.


### Integration
The code that handles the Helper Service inforation has been provided for you.

![capture](https://user-images.githubusercontent.com/42374797/51045249-11033000-15bb-11e9-8b4f-4ba251becf1a.PNG)

       
