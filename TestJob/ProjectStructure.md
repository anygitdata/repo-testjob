# Структура проекта в разрезе технического задания
## Описание структуры ИЗМЕНЯЕТСЯ по результатам доработок решения

-------------------------------------------

# Используемое ПО:
	- VS 2019
	- C#, ASP.NET CORE MVC 5.12
	- MS SQL 2016
	- jQuery v:3.5.1
	- Bootstrap v:4.3.1

------------------------------------------

# Структура решения:
## TestJob основной проект
## BaseSettingsTests.Tests
	Проект xUnit для тестирования базовых моделей, классов, методов
## TestJob.Tests
	Проект xUnit для тестирования объектов User API

------------------------------------------

# Buisness
## Controllers.HomeController 

## Rest API
### Controllers.ProjectsController	Rest API controller for Projects
### Controllers.TasksController		Rest API controller for Tasks
### Controllers.DescController		Rest API controller for Comments

### Controllers.RestController		Rest API controller for any data

------------------------------------------

#  DAL (Data Access Layer)
## Model.Project модель проекта
## Model.Task	 модель задача
## Model.TaskComments модель описаниеЗадачи 
Может быть в виде файла или текстового поля

------------------------------------------

# ООП (объектно ориентированное программирование)
Models.ModelViews/* отдельная директория для объекта

Модель наследования:
baseClass-> abstractClass->abstractClass(если необходимо)->classImplementation

abstract and virtual functions

------------------------------------------

# User API
## Models.UserAPI/**    софт для программирования

## Debug
Режим debug используется для отладки и тестирования на уровне браузера и xUnit tests
## Settings
Настройка через файл wwwroot/Settings/settings.json
```
{"seedData":"off","maxSizeFile":400,"debug":"off"}
```

key:seedData для загрузки начальных данных
считывание параметра ОДНОРАЗОВОЕ.
Если изначально значение "on" после считывания "off"

