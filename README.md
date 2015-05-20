# Kyutek
Repo for Nar-Shadda team project: Creating a text-based console game with C#. Deadline: 27.05.2015

Как се наглася Git (cmd версия):

Инсталирате Гит - http://git-scm.com/ - тази стъпка е нужна независимо дали ще използвате CMD или TortoiseGit

Конфигурирате Гит на машината си (в cmd):

git config --global user.name "YOUR NAME"

git config --global user.email "YOUR EMAIL ADDRESS"

Отивате в директорията, където ще си правите локално repository.

cd C:\baba\ Ако е на друг драйв, пишете първо D:, за да се преместите на него. След това - cd ....

Инициализирате Git

git init => създава ви се скрита .git папка.

Казвате "това място е за онова място":

git remote add origin ---url--- - ще наричаме origin отдалеченото repository; URL заменяте с clone url-a, който GitHub ви дава.

Готови сте!

=> Как се работи с Гит?

Интересуват ви 4-на команди:

git pull -u origin master - с други думи, git pull от origin в master. (или дърпаме нещата при нас) git add . - прибавяме всички променени файлове в нов commit, готов за пращане. Ако искаме специфични да прибавим, пишем ги поименно вместо точката.

git commit -m "blablabla" - приготвяме commit (принос към repository-то в GitHub) със съобщение "блабла". Важно: не пишете "блабла", ами конкретно какво променяте! git push - качвате всичко в главното repository. Ако то е на по-нова версия от вас, трябва първо да направите git pull ... - и ако има конфликти, трябва да се справите с тях. git status - когато се чудите какво става с гит. Чисто информативно. Успех!
