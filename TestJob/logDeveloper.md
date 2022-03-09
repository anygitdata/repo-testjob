
# Журнал разработки (09.03.2022)


# Страница "Создание задачи" (09.03.2022)
CreateTask02.chhtml используется для создания новой задачи
span.#spn-date-time-pr and span.#spn-title-pr заполнение через js


```
class GenTaskView
	public Guid ProjectId { get; set; }
	public string TaskId { get; set; }
	public string TaskName { get; set; }
	public string Date { get; set; }
	public string Time { get; set; }


class  GenTaskViewExt: GenTaskView
	public string DateExt { get; set; }
	public string TimeExt { get; set; }

```

updtask/A13A3816-FFA2-4F69-AF12-D5075BF66FBA


