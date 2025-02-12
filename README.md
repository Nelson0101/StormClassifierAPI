# StormDamageClassifierAPI
The goal of this project is to develop a backend application using ASP.NET Core that integrates with a classification algorithm previously developed in the Neural Network course (NAIL002). This algorithm is designed to classify the extent of storm damage in Switzerland based on weather data and the main process which cause the damage (landslide, mudslide or collapse) . The project focuses on building a robust and efficient API to get access to storm damage classifications, where the accuracy of the classifier should not be part of this project.
For more information about the classification algorithm, please see [Presentation](https://github.com/Nelson0101/StormClassifierAPI/blob/main/EndTermPresentation_NilsGaemperli.pdf).

## User Documentation
To query the Classifier, the application must be running and the following url can be queried: **http://localhost:{port}/api/damage/{city}/{date}/{main_process}**
Where: 
port: The port on which the application is running. This is displayed in the console when the application is sucsessfully started and running.
- city: The **Swiss** city which you want to have a classification of a storm damage
- date: The date you want to have the classification. This date must lay 2d days in the past, as newer weather is not supported by the [Open-Meto Archive](https://open-meteo.com/en/docs/historical-weather-api)
- main_process: The main process which caused the damage. This can be: (water = 1), (landslide = 2) or (collapse = 3). For further information consider [Presentation](https://github.com/Nelson0101/StormClassifierAPI/blob/main/EndTermPresentation_NilsGaemperli.pdf)

So a url could be "http://localhost:5250/api/damage/Zurich/2025-02-04/1"

### Response
The response will be in JSON in the following schema:
```JSON
  {"classification": "damage_categroy"}
```
The damage categories are:
- Small (0): [10’000 ; 400’000[ CHF
- Medium (1): [400’000 - 2’000’000[ CHF
- Large (2): > 2’000’000 or death

For the above mentioned query, the response is:
```JSON
  {"classification": 0}
```

## Architecture
This prject uses the Clean Architecture approach by Uncle Bob. The package names correspond to the layer in the clean architecture. Please consider the packages / directories for the information about the layer of a class. This allows the README to be dynamic.
- Entities -> Domain
- Use Cases -> Application
- Interface Adapter -> Infrastructure
- Web -> Presentation
  
![CleanArchitecture.jpg (772×567)](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

As ASP.Net uses dependency injection by default, this pattern is further used through the whole project. The following diagram displays the injections of the most important classes.
![image](https://github.com/user-attachments/assets/fcf7f647-f4d2-4e71-9e70-e34fd0aeb7f5)



