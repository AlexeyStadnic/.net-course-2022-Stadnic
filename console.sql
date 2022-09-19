/*
 1. провести выборки клиентов, у которых сумма на счету ниже
определенного значения, отсортированных в порядке возрастания суммы
 */
select name
from client
where id in
      (select client_id from account
                        where amount < 800
                        order by amount);

/*
2. провести поиск клиента с минимальной суммой на счете;
 */
select name
from client
where id in
      (select client_id from account
                        where amount = (select min(amount) from account));

/*
Либо, если нам нужны клиенты с минимальным счетом определенной валюты
 */
select name
from client
where id in
      (select client_id from account
                        where amount = (select min(amount) from account
                                                           where account.name = 'EUR'));

/*
3. провести подсчет суммы денег у всех клиентов банка;
 */
select sum(amount)
from account
where name = 'USD';

/*
4. с помощью оператора Join, получить выборку банковских счетов и
их владельцев;
 */
select account.name, account.amount, client.name
from account join client on id = account.client_id;

/*
5. вывести клиентов от самых старших к самым младшим;
 */
select name
from client
order by birthday;

/*
6.  подсчитать количество человек, у которых одинаковый возраст;
 */
select count(birthday)
from client
where extract(YEAR from birthday) = '1967';

/*
7. сгруппировать клиентов банка по возрасту;
 */
select extract(Year from birthday), count(extract(Year from birthday))
from client
group by extract(Year from birthday);

/*
8. вывести только N человек из таблицы.
 */
select name
from client
limit 5;

