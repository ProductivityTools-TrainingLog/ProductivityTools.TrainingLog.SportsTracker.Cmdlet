<!--Category:Powershell--> 
 <p align="right">
    <a href="https://www.powershellgallery.com/packages/ProductivityTools.SportsTracker/"><img src="Images/Header/Powershell_border_40px.png" /></a>
    <a href="http://productivitytools.tech/sports-tracker-cmdlet//"><img src="Images/Header/ProductivityTools_green_40px_2.png" /><a> 
    <a href="https://github.com/ProductivityTools-TrainingLog/ProductivityTools.TrainingLog.SportsTracker.Cmdlet"><img src="Images/Header/Github_border_40px.png" /></a>
</p>
<p align="center">
    <a href="http://productivitytools.tech/">
        <img src="Images/Header/LogoTitle_green_500px.png" />
    </a>
</p>

# TrainingLog-SportTracker Cmdlet
 
PowerShell module allows to import trainings from TrainingLog to SportsTracker website

<!--more-->
It exposes following commands (TO BE CORECTED, more parameters needed)

 ```powershell
Export-TrainingsToSportTracker -Verbose -Login "login" -Password "password" -Account "account" -TrainingLogApiAddress "http:\\example\"
```
Command can take parameters from master configuration. 

```json
{
  "login": "pwujczyk@gmail.com",
  "password": "my secret password",
  "trainingLogApiAddress":"https://localhost:5001"
}

```

Login and Password can be provided with [PowerShell Master Configuration](http://productivitytools.tech/powershell-master-configuration/) or parameter.

## Example
```powershell
Export-TrainingsToSportTracker -Verbose -Account pwujczyk
```
 

 <!--og-image-->
 ![Example](Images/TrainingAdded.png)
 
