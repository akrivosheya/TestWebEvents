# TestWebEvents

Тестовое задание по отправлению данных о событии на удаленный сервер.

При возникновении событий используется специальный статический класс Messenger, который был позаимствован.

Данные о событиях хранятся в отдельной очереди и через интервалы времени обрабатываются для отправки на сервер.

Данные событий при завершении игры могут сохраниться в файле в формате JSON и выгрузиться при запуске приложения.

К сожалению я не правильно понял условие по кулдауну и принял его за интервалы оптравки событий. Также в моем коде не предусмотрено сохранений событий при неожиданном завершении приложения.