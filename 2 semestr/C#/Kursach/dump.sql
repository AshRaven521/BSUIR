-- MySQL dump 10.13  Distrib 8.0.20, for Win64 (x86_64)
--
-- Host: localhost    Database: kursach
-- ------------------------------------------------------
-- Server version	8.0.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `testnumbers`
--

DROP TABLE IF EXISTS `testnumbers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `testnumbers` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `testnumbers`
--

LOCK TABLES `testnumbers` WRITE;
/*!40000 ALTER TABLE `testnumbers` DISABLE KEYS */;
INSERT INTO `testnumbers` VALUES (1,'Test1'),(2,'Test2'),(3,'Test3');
/*!40000 ALTER TABLE `testnumbers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `testtasks`
--

DROP TABLE IF EXISTS `testtasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `testtasks` (
  `id` int NOT NULL,
  `task` varchar(1000) NOT NULL,
  `answers` varchar(1000) NOT NULL,
  `testid` int NOT NULL,
  `correctanswerid` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `testtasks`
--

LOCK TABLES `testtasks` WRITE;
/*!40000 ALTER TABLE `testtasks` DISABLE KEYS */;
INSERT INTO `testtasks` VALUES (1,'Что из перечисленного не подходит под ограничение class?','a) делегаты; b) интерфейсы; c) массивы; d) перечисления',1,4),(2,'Какой тип имеет литерал 5D?','a) double; b) int; c) decimal; d) string',1,1),(3,'Какой модификатор допустим у параметров перегрузки операции?','a) in; b) ref; c) out; d) parama',1,1),(4,'Какой тип имеет выражение \"abcde\"[3]?','a) string; b) char; c) string[]; d) ошибка компиляции',1,2),(5,'Может ли структура иметь статические методы?','a) нет; b) да, но только для реализации интерфейса; c) да; d) да, но только private',1,3),(6,'Что произойдет, если у конструктора класса не указан вызов другого конструктора? ','a) будет ошибка компиляции;  b) такой конструктор нельзя будет вызвать; c) компилятор подставит вызов : base(); d) компилятор подставит вызов this() ',1,3),(7,'Что из перечисленного всегда подходит под ограничение unmanaged?','a) ни структуры, ни перечисления; b) только структуры; c) только перечисления; d) и структуры, и перечисления',1,3),(8,'Какой из этих типов не может участвовать в операциях упаковки?','a) string; b) DateTime; c) bool ;d) decimal',1,1),(9,'Что из перечисленного не входит в возможности CLR?','a) автоматическая сборка мусора; b) автоматическое расширение памяти программы до произвольного размера; c) проверка всех операций проведения типов; d) изоляция работающих прложений друг от друга',1,2),(10,'Что из перечисленного является отличием C# от C++ ?','a) размер стандартных целочисленных типов зафиксирован в спецификации; b) структуры могут содержать методы; c) если тело цикла состоит из одной инструкции, фигурные скобки можно опустить; d) у инструкции try может быть сколько угодно секций catch',1,1),(11,'Исключение какого типа стоит использовать, если значение, переданное в метод, отрицательное, а требуется положительное?','a) FormatException; b) NotimplementedException; c) ArgumentException; d) InvalidOperationException',2,3),(12,'Какой тип имеет выражение (new int[10,10])[100]?','a) не имеет типа, потому что индекс выходит за пределы массива; b) int[]; c) int;  d) ошибка компиляции',2,4),(13,'Что из перечисленного верно про свойство, возвращающее по ссылке?','a) оно обязано иметь тело-выражение; b) оно обязано быть только для чтения; c) оно обязано быть авореализуемым; d) оно обязано быть примитивного типа',2,2),(14,'Что из перечисленного верно про переменные, объявленные с ключевым словом var?','a) такая переменная всегда имеет определенный тип; b) такая переменная не может иметь тип делегата; c) объявление var эквивалентно объявлению object с точки зрения CLR; d) они доступны только для чтения',2,1),(15,'В каких из этих мест можно писать директиву using для подключения пространства имен?','a) в начале файла или объявления пространства имён; b) в начале файла, объявления пространства имен или типа; c) в начале файла; d) в начале файла, объявления пространства имён, типа или тела метода',2,1),(16,'Какие из этих методов можно вызвать с помошью оператора ?.  ?','a) ни экземплярные, ни статические; b) только статические; c) и экземплярные, и статические; d) только экземплярные ',2,4),(17,'У каких из этих типов можно задавать имена или псевдонимы для членов?','a) у анонимных типов имена, у ValueTuple псевдонимы; b) у анонимных типов имена, у Tuple псевдонимы; c) у ValueTuple имена, у анонимных типов псевдонимы; d) у ValueTuple имена, у Tuple псевдонимы',2,1),(18,'Что сделает инструкция Console.WriteLine($\"{x}\"); , если переменная x не объявлена?','a) ошибка времени выполнения; b) ошибка компиляции; c) выведет строку x; d) выведет пустую строку',2,2),(19,'В каком из этих случаев тип переменной нельзя заменить на var, не изменив логику программы?','a) int x = 10; b) char x = \'\\n\'; c) double x = 100; d) bool x = true',2,3),(20,'Какое из этих ключевых слов может стоять после слова out?','a) if; b) return; c) private; d) var',2,4),(21,'Какой из этих методов класса Object является protected?','a) GetType; b) MemberwiseClone; c) Equals; d) GetHashCode',3,2),(22,'Может ли перечисление иметь статичекие методы?','a) да, но только private; b) да; c) да, но только для реализации интерфейса; d) нет',3,4),(23,'Что из перечисленного справедливо про JIT-компиляцию?','a) она может использовать специфические инструкции того CPU, на котором работает; b) она позволяет сэкономить время, компилируя всю сборку целиком; c) она является необходимым звеном для механизма сборки мусора; d) она компилирует метод при каждом вызове ',3,1),(24,'Что из перечисленного верно про конструкторы пользовательского класса?','a) хотя бы один из них не имеет параметров; b) хотя бы один из них является public; c) хотя бы один из них является виртуальным; d) хотя бы один из них вызывает конструктор базового класса ',3,4),(25,'Какой из перечисленных модификаторов применим к структурам? ','a) abstract; b) static; c) sealed; d) readonly',3,4),(26,'Вместе с какими из этих ограничений на шаблонные параметры можно объявить ограничение new() ?','a) class, struct и unmanaged; b) class и struct; с) class; d) struct и unmanged',3,3),(27,'Что из перечисленного является отличием C# от C++ ?','a) шаблонные параметры могут быть и у типов, и у функций (методов); b) стандарт языка поддерживает события; c) операции приведения типов могут быть перегружены; d) у членов класса можно задать область видимости ',3,2),(28,'Что из перечисленного может использовать замыкания?','a) анонимные методы, лямбда-выражения, локальные функции и обычные методы; b) лямбда-выражения; c) анонимные методы и лямбда-выражения; d) анонимные методы, лямбда-выражения и локальные функции',3,4),(29,'Какой из этих модификаторов параметров никогда не пишется при вызове метода?','a) in; b) out; c) ref; d) params',3,4),(30,'Имеется код  public class A { public class B { private int f ; } }  Какие классы могут обращаться к полю f ?','a) и A, и B; b) только B; c) ни A, ни B; d) только A',3,2);
/*!40000 ALTER TABLE `testtasks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'kursach'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-27 13:23:51
