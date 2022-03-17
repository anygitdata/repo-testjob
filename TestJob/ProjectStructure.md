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

Models.ModelViews/* отдельные директории для объектов
## Models.ModelViews.ComnView
	Модуль наследования: GeneralModelView_templ->GenModelViewComn
	TaskComment_ModelView модель для View
	
## Models.ModelViews.ProjectView
	Модель наследования: GenProjectView_templ базовый класс для
		GenProjectView_add   and   GenProjectView_upd

## Models.ModelViews.TaskView
	Модель наследования: GenTaskView_templ базовый класс для
		GenTaskView_create при создании новой задачи
		GenTaskView_modf базовый класс для
			GenTaskView_update  and GenTaskView_cancel

------------------------------------------

# User API
## Models.UserAPI/**    софт для программирования

## Debug
Режим debug используется для отладки и тестирования на уровне браузера и xUnit tests

## Settings
Настройка через файл wwwroot/Settings/settings.json
```
{"seedData":"on/off","maxSizeFile":400,"debug":"on/off", "test":"on/off"}
```

key:seedData для загрузки начальных данных
считывание параметра ОДНОРАЗОВОЕ.
Если изначально значение "on" после считывания "off"

test: on -> блокирует верификацию на уровне браузера. Верификация на сервере

