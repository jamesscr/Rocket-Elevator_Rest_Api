# ROCKET ELEVATORS REST API

## Week 8
We implemented a REST API in C# that is capable of manipulating various entity's within the operational database and deployed in via Microsoft Azure. 

## Our Team
  - Olivier Godbout - Team Leader
  - Samuel Chabot  - Collaborator 
  - Colin Larke - Collaborator 
  - James Allan Jean-Jacques - Collaborator

 ## INSTRUCTIONS TO FOLLOW
 
 ## 1 - Click link to open Postman and access the collections needed for the queries
[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/3f98d5e6a531e3025b47)


## 2 - On the left panel click Collections and Open the Folder W8-OlivierGodbout
The sub-folder RestAPI contains the queries you'll want to run, open it up and select one. For GET/PUT queries that target a single entity you can change the target id in the URL next to GET/PUT(Whichever one your query is doing)</br>
`https://imastuden.azurewebsites.net/api/elevators/<CHANGE ME>`</br>
To change the status on PUT requests click on the body tab in the upper panel and change the status to either "Active", "Inactive" or "Intervention"
 
 ![View of Postman](https://i.imgur.com/Um1JCw5.png)
 
## 3 - When your parameter is set click the blue send button to run the query. You can view the results by clicking the lower Body tab in the response panel.
![View of Postman](https://i.imgur.com/25Dn8l5.png)

## You can also take view some of the request directly via the Azure deployment in the links below
**Check the status of an elevator -** http://imastuden.azurewebsites.net/api/elevators/1 \
**Get the list of elevator that are not in operation -** http://imastuden.azurewebsites.net/api/inactiveelevators \
**Check the status of a column -** http://imastuden.azurewebsites.net/api/columns/1 \
**Check the status of a battery -** http://imastuden.azurewebsites.net/api/batteries/1 \
**Get list of buildings that have a battery/column/elevator intervention -** http://imastuden.azurewebsites.net/api/buildings \
**Retrieve a list of Leads from the last 30 days are not yet customers -** http://imastuden.azurewebsites.net/api/leads \
**Retrieve a list of quotes with for commercial buildings with excellium products -** http://imastuden.azurewebsites.net/api/quotes



# Rocket-Elevator_Rest_Api
