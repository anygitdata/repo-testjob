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
Решение построено с учетом удобства сопровождения и модульного тестирования


## TestJob основной проект

## BaseSettingsTests.Tests
	Проект xUnit модульные тесты базовых моделей, классов, методов 
	и API настроек модульных тестов
## TestController.Test
	Проект xUnit модульные тесты Controllers
## TestJob.Tests
	Проект xUnit модульные тесты User API

------------------------------------------

# Controllers
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

# Модели входящих/исходящих запросов (View model)

Models.ModelViews/* отдельные директории объектов

## Models.ModelViews.ComnView	(Модель описаний задач)
	GeneralModelView_templ<TModel, TResult, TView> Базовый шаблон
		GenModelViewComn: GeneralModelView_templ
	

## Models.ModelViews.ProjectView	(модель проекта)
	(Шаблон по аналогии с GeneralModelView_templ)
	Модель наследования: GenProjectView_templ базовый класс:
		GenProjectView_add: GenProjectView_templ
		GenProjectView_upd: GenProjectView_templ


## Models.ModelViews.TaskView		(модель задач)
	Модель наследования: GenTaskView_templ базовый шаблон
		новая задача
		GenTaskView_create : GenTaskView_templ 


		GenTaskView_modf: GenTaskView_templ
			GenTaskView_update : GenTaskView_modf
			GenTaskView_cancel : GenTaskView_modf

------------------------------------------

# User API
## Models.UserAPI/**    софт для программирования

------------------------------------------

# Settings

Настройки через файл wwwroot/Settings/settings.json
```
{"seedData":"on/off","maxSizeFile":400,"debug":"on/off", "test":"on/off"}
```

## seedData
при значении "on" -> загрузка начальных данных
считывание параметра ОДНОРАЗОВОЕ.
Если изначально значение "on" после считывания "off"


## Debug
Режим используется для отладки и тестирования на уровне браузера и модульных тестов xUnit

При включаенном Debug ВСЕ ОПЕРАЦИИ по БД блокируются.
Используется также для отладки ajax request


## test
test: on -> переключает верификацию на сервер

-----------------------------------------------------

# js (jQuery) wwwroot/js/**
ВСЕ скрипты используют 'шаблон модуль'

base.values.js  базовый скрипт, содержит общие переменные/константы, методы


скрипты пользовательского интерфейса:

modf.project.js создание/изм проекта  
task.create.js  создание задачи
create.taskcomment.js создание/изм/удаление описаний задач
	плюс редактор изм/удаления задачи

ВСЕ скрпты осуществляют верификацию введенных данных
(При включенном Debug результаты выводятся на консоль браузера)


# SASS -> CSS
wwwroot/SASS/**