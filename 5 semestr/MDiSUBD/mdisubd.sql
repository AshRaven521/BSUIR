CREATE DATABASE  IF NOT EXISTS `mdisubd` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `mdisubd`;
-- MySQL dump 10.13  Distrib 8.0.20, for Win64 (x86_64)
--
-- Host: localhost    Database: mdisubd
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
-- Table structure for table `car_photo`
--

DROP TABLE IF EXISTS `car_photo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `car_photo` (
  `id` int NOT NULL,
  `path` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `car_photo`
--

LOCK TABLES `car_photo` WRITE;
/*!40000 ALTER TABLE `car_photo` DISABLE KEYS */;
INSERT INTO `car_photo` VALUES (1,'c:'),(2,'asd'),(3,'qwe'),(4,'tyu'),(5,'tgf');
/*!40000 ALTER TABLE `car_photo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customer` (
  `id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(45) NOT NULL,
  `last_name` varchar(45) NOT NULL,
  `telephone_number` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer` VALUES (1,'Vasya','Pupkin','+375291111111'),(2,'Ivan','Ivanov','+375291234567'),(3,'Alexey','Prostoyev','+375445674398'),(4,'Mikhail','Trobov','+375446578108'),(5,'Vladimir','Dvorechin','+375290912129'),(6,'Vova','Krotov','+375446758932');
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detail`
--

DROP TABLE IF EXISTS `detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detail` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  `producer` varchar(45) NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detail`
--

LOCK TABLES `detail` WRITE;
/*!40000 ALTER TABLE `detail` DISABLE KEYS */;
INSERT INTO `detail` VALUES (1,'Engine','VW',5000),(2,'Transmission','Audi',2200),(3,'Clutch','BMW',650),(4,'Release bearing','Seat',35),(5,'Shock absorbers','Peugeout',45);
/*!40000 ALTER TABLE `detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master`
--

DROP TABLE IF EXISTS `master`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `master` (
  `id` int NOT NULL,
  `first_name` varchar(45) NOT NULL,
  `last_name` varchar(45) NOT NULL,
  `category` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master`
--

LOCK TABLES `master` WRITE;
/*!40000 ALTER TABLE `master` DISABLE KEYS */;
INSERT INTO `master` VALUES (1,'Roma','Schupkin','6'),(2,'Valera','Gretov','4'),(3,'Maksim','Frolov','3'),(4,'Kostya','Chorov','6'),(5,'Denis','Gromov','5');
/*!40000 ALTER TABLE `master` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_customer` int NOT NULL DEFAULT '0',
  `id_workshop` int NOT NULL DEFAULT '0',
  `number` varchar(10) NOT NULL,
  `id_detail` int NOT NULL DEFAULT '0',
  `id_car_photo` int NOT NULL DEFAULT '0',
  `id_master` int NOT NULL DEFAULT '0',
  `id_user` int NOT NULL DEFAULT '0',
  `id_work_type` int NOT NULL DEFAULT '0',
  `id_payment_type` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `id_idx` (`id_car_photo`),
  KEY `id_idx2` (`id_detail`),
  KEY `id_idx3` (`id_master`),
  KEY `id_idx5` (`id_work_type`),
  KEY `id_idx6` (`id_workshop`),
  KEY `id_1` (`id_car_photo`),
  KEY `id_2` (`id_customer`),
  KEY `id_3` (`id_detail`),
  KEY `id_4` (`id_master`),
  KEY `id_5` (`id_user`),
  KEY `id_6` (`id_work_type`),
  KEY `id_7` (`id_workshop`),
  KEY `1` (`id_car_photo`),
  KEY `2` (`id_customer`),
  KEY `3` (`id_detail`),
  KEY `4` (`id_master`),
  KEY `5` (`id_user`),
  KEY `6` (`id_work_type`),
  KEY `7` (`id_workshop`),
  KEY `id_pay_idx` (`id_payment_type`),
  CONSTRAINT `id_car` FOREIGN KEY (`id_car_photo`) REFERENCES `car_photo` (`id`),
  CONSTRAINT `id_det` FOREIGN KEY (`id_detail`) REFERENCES `detail` (`id`),
  CONSTRAINT `id_mast` FOREIGN KEY (`id_master`) REFERENCES `master` (`id`),
  CONSTRAINT `id_pay` FOREIGN KEY (`id_payment_type`) REFERENCES `payment_type` (`id`),
  CONSTRAINT `id_us` FOREIGN KEY (`id_user`) REFERENCES `user` (`id`),
  CONSTRAINT `id_work_typ` FOREIGN KEY (`id_work_type`) REFERENCES `work_type` (`id`),
  CONSTRAINT `id_worksho` FOREIGN KEY (`id_workshop`) REFERENCES `workshop` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,1,1,'no.1',1,1,1,1,1,1),(2,2,2,'no.2',2,2,2,2,2,2),(3,3,3,'no.3dsdasa',1,3,3,3,3,3),(22,3,1,'0',1,1,1,7,1,1),(24,1,1,'0',1,1,1,9,1,1),(26,1,1,'11111',1,1,1,7,2,1);
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_type`
--

DROP TABLE IF EXISTS `payment_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payment_type` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_type`
--

LOCK TABLES `payment_type` WRITE;
/*!40000 ALTER TABLE `payment_type` DISABLE KEYS */;
INSERT INTO `payment_type` VALUES (1,'Cash'),(2,'Card'),(3,'Cash'),(4,'Card'),(5,'Card');
/*!40000 ALTER TABLE `payment_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `is_admin` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'bur@mail.ru','1111',0),(2,'bom@inbox.com','1234',0),(3,'dub@mail.com','5678',0),(4,'jef@mail.ru','2345',0),(5,'koma@tut.by','5432',0),(6,'jhyt@mail.ru','9876',0),(7,'qas','was',0),(8,'poik@inbox.ru','8769',0),(9,'admin','admin',1),(10,'lpo@mail.ru','76543',0);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_action`
--

DROP TABLE IF EXISTS `user_action`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_action` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `time` varchar(45) NOT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=184 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_action`
--

LOCK TABLES `user_action` WRITE;
/*!40000 ALTER TABLE `user_action` DISABLE KEYS */;
INSERT INTO `user_action` VALUES (1,'Пользователь вошел в аккаунт','15.10.2021 20:23:08',0),(2,'Пользователь вошел в аккаунт','15.10.2021 20:41:46',7),(3,'Пользователь вошел в аккаунт','15.10.2021 20:55:56',2),(4,'Добавлен заказ','15.10.2021 20:56:52',0),(5,'Пользователь вошел в аккаунт','15.10.2021 20:59:47',2),(6,'Добавлен заказ','15.10.2021 21:00:43',2),(7,'Пользователь вошел в аккаунт','15.10.2021 21:04:04',1),(8,'Пользователь вошел в аккаунт','15.10.2021 21:10:09',5),(9,'Польщователь зарегестрировался','15.10.2021 21:19:17',8),(10,'Добавлен заказ','15.10.2021 21:19:36',8),(11,'Пользователь вошел в аккаунт','15.10.2021 21:28:12',7),(12,'Добавлен заказ','15.10.2021 21:28:25',7),(13,'Пользователь вошел в аккаунт','15.10.2021 21:30:01',7),(14,'Пользователь вошел в аккаунт','15.10.2021 21:30:58',7),(15,'Пользователь вошел в аккаунт','15.10.2021 21:32:44',7),(16,'Добавлен заказ','15.10.2021 21:33:21',7),(17,'Пользователь вошел в аккаунт','15.10.2021 21:36:43',7),(18,'Заказ удален','15.10.2021 21:37:01',7),(19,'Пользователь вошел в аккаунт','15.10.2021 21:39:42',7),(20,'Пользователь вошел в аккаунт','15.10.2021 21:41:32',7),(21,'Пользователь вошел в аккаунт','15.10.2021 21:55:07',7),(22,'Выполнена сортировка по имени заказчика','15.10.2021 21:55:11',7),(23,'Выполнена сортировка по цене','15.10.2021 21:55:16',7),(24,'Выполнен поиск по заданному параметру','15.10.2021 21:55:29',7),(25,'Выполнен поиск по заданному параметру','15.10.2021 21:55:58',7),(26,'Выполнена сортировка по имени заказчика','15.10.2021 21:56:43',7),(27,'Заказ удален','15.10.2021 21:56:52',7),(28,'Заказ удален','15.10.2021 21:57:02',7),(29,'Заказ удален','15.10.2021 21:57:09',7),(30,'Пользователь вошел в аккаунт','15.10.2021 21:58:25',7),(31,'Добавлен заказ','15.10.2021 21:58:36',7),(32,'Заказ удален','15.10.2021 21:58:57',7),(33,'Пользователь вошел в аккаунт','15.10.2021 21:59:17',7),(34,'Пользователь вошел в аккаунт','15.10.2021 22:00:47',7),(35,'Добавлен заказ','15.10.2021 22:00:51',7),(36,'Заказ удален','15.10.2021 22:01:37',7),(37,'Пользователь вошел в аккаунт','15.10.2021 22:02:15',7),(38,'Добавлен заказ','15.10.2021 22:02:19',7),(39,'Заказ удален','15.10.2021 22:03:44',7),(40,'Пользователь вошел в аккаунт','15.10.2021 22:04:37',7),(41,'Заказ удален','15.10.2021 22:04:42',7),(42,'Добавлен заказ','15.10.2021 22:04:48',7),(43,'Пользователь вошел в аккаунт','15.10.2021 22:08:35',7),(44,'Заказ удален','15.10.2021 22:08:41',7),(45,'Добавлен заказ','15.10.2021 22:08:47',7),(46,'Заказ удален','15.10.2021 22:08:51',7),(47,'Пользователь вошел в аккаунт','15.10.2021 22:09:52',7),(48,'Пользователь вошел в аккаунт','15.10.2021 22:10:10',7),(49,'Заказ удален','15.10.2021 22:10:14',7),(50,'Добавлен заказ','15.10.2021 22:10:17',7),(51,'Заказ удален','15.10.2021 22:10:21',7),(52,'Пользователь вошел в аккаунт','15.10.2021 22:12:09',7),(53,'Добавлен заказ','15.10.2021 22:12:14',7),(54,'Заказ удален','15.10.2021 22:12:18',7),(55,'Польщователь зарегестрировался','15.10.2021 22:16:10',9),(56,'Польщователь зарегестрировался','15.10.2021 22:26:19',10),(57,'Пользователь вошел в аккаунт','15.10.2021 22:35:23',7),(58,'Выполнена сортировка по цене','15.10.2021 22:35:34',7),(59,'Выполнена сортировка по имени заказчика','15.10.2021 22:35:37',7),(60,'Выполнена сортировка по цене','15.10.2021 22:35:41',7),(61,'Добавлен заказ','15.10.2021 22:35:54',7),(62,'Выполнена сортировка по цене','15.10.2021 22:35:56',7),(63,'Выполнена сортировка по имени заказчика','15.10.2021 22:35:59',7),(64,'Выполнена сортировка по цене','15.10.2021 22:36:03',7),(65,'Пользователь вошел в аккаунт','15.10.2021 22:55:02',9),(66,'Пользователь вошел в аккаунт','15.10.2021 22:56:14',7),(67,'Пользователь вошел в аккаунт','15.10.2021 22:57:19',7),(68,'Добавлен заказ','15.10.2021 22:57:29',7),(69,'Пользователь вошел в аккаунт','15.10.2021 22:59:30',9),(70,'Добавлен заказ','15.10.2021 22:59:36',9),(71,'Пользователь вошел в аккаунт','15.10.2021 23:25:50',9),(72,'Пользователь вошел в аккаунт','15.10.2021 23:28:03',9),(73,'Пользователь вошел в аккаунт','15.10.2021 23:28:50',9),(74,'Пользователь вошел в аккаунт','15.10.2021 23:30:14',9),(75,'Пользователь вошел в аккаунт','15.10.2021 23:31:42',9),(76,'Пользователь вошел в аккаунт','15.10.2021 23:32:49',9),(77,'Пользователь вошел в аккаунт','15.10.2021 23:34:08',9),(78,'Пользователь вошел в аккаунт','15.10.2021 23:36:41',9),(79,'Пользователь вошел в аккаунт','15.10.2021 23:39:36',9),(80,'Пользователь вошел в аккаунт','15.10.2021 23:40:08',9),(81,'Пользователь вошел в аккаунт','15.10.2021 23:41:26',1),(82,'Пользователь вошел в аккаунт','15.10.2021 23:42:23',9),(83,'Пользователь вошел в аккаунт','15.10.2021 23:44:12',9),(84,'Выведен журнал действий','15.10.2021 23:44:34',9),(85,'Выведен журнал действий','15.10.2021 23:44:38',9),(86,'Пользователь вошел в аккаунт','16.10.2021 9:18:08',7),(87,'Добавлен заказ','16.10.2021 9:18:26',7),(88,'Выполнена сортировка по имени заказчика','16.10.2021 9:18:40',7),(89,'Выполнена сортировка по цене','16.10.2021 9:18:46',7),(90,'Выполнен поиск по заданному параметру','16.10.2021 9:18:57',7),(91,'Выполнена сортировка по цене','16.10.2021 9:19:00',7),(92,'Выполнен поиск по заданному параметру','16.10.2021 9:19:08',7),(93,'Пользователь вошел в аккаунт','16.10.2021 9:19:28',9),(94,'Заказ удален','16.10.2021 9:19:31',9),(95,'Выведен журнал действий','16.10.2021 9:19:45',9),(96,'Пользователь вошел в аккаунт','16.10.2021 9:21:15',9),(97,'Заказ удален','16.10.2021 9:21:22',9),(98,'Выполнена сортировка по имени заказчика','16.10.2021 9:21:27',9),(99,'Выполнен поиск по заданному параметру','16.10.2021 9:21:38',9),(100,'Пользователь вошел в аккаунт','16.10.2021 9:23:30',9),(101,'Выполнен поиск по заданному параметру','16.10.2021 9:24:26',9),(102,'Выполнена сортировка по цене','16.10.2021 9:24:38',9),(103,'Выполнен поиск по заданному параметру','16.10.2021 9:24:59',9),(104,'Выполнена сортировка по имени заказчика','16.10.2021 9:25:07',9),(105,'Выполнен поиск по заданному параметру','16.10.2021 9:25:19',9),(106,'Выполнена сортировка по имени заказчика','16.10.2021 9:25:28',9),(107,'Выполнен поиск по заданному параметру','16.10.2021 9:25:48',9),(108,'Выполнена сортировка по имени заказчика','16.10.2021 9:25:55',9),(109,'Пользователь вошел в аккаунт','16.10.2021 9:36:41',9),(110,'Пользователь вошел в аккаунт','16.10.2021 9:37:59',9),(111,'Выполнен поиск по заданному параметру','16.10.2021 9:39:09',9),(112,'Выполнена сортировка по имени заказчика','16.10.2021 9:39:19',9),(113,'Выполнена сортировка по имени заказчика','16.10.2021 9:39:20',9),(114,'Выполнен поиск по заданному параметру','16.10.2021 9:40:03',9),(115,'Пользователь вошел в аккаунт','16.10.2021 9:40:54',9),(116,'Выполнен поиск по заданному параметру','16.10.2021 9:43:42',9),(117,'Пользователь вошел в аккаунт','16.10.2021 9:44:03',9),(118,'Выполнен поиск по заданному параметру','16.10.2021 9:44:15',9),(119,'Пользователь вошел в аккаунт','16.10.2021 9:44:40',9),(120,'Выполнен поиск по заданному параметру','16.10.2021 9:45:20',9),(121,'Пользователь вошел в аккаунт','16.10.2021 9:45:59',9),(122,'Выполнен поиск по заданному параметру','16.10.2021 9:46:44',9),(123,'Пользователь вошел в аккаунт','16.10.2021 9:57:12',9),(124,'Выполнен поиск по заданному параметру','16.10.2021 9:57:25',9),(125,'Выполнена сортировка по имени заказчика','16.10.2021 9:57:28',9),(126,'Выполнен поиск по заданному параметру','16.10.2021 9:57:36',9),(127,'Выполнена сортировка по имени заказчика','16.10.2021 9:57:48',9),(128,'Выполнен поиск по заданному параметру','16.10.2021 9:57:55',9),(129,'Выполнена сортировка по имени заказчика','16.10.2021 9:58:00',9),(130,'Выполнен поиск по заданному параметру','16.10.2021 9:58:49',9),(131,'Выполнен поиск по заданному параметру','16.10.2021 9:59:03',9),(132,'Выполнен поиск по заданному параметру','16.10.2021 9:59:04',9),(133,'Выполнен поиск по заданному параметру','16.10.2021 9:59:04',9),(134,'Выполнен поиск по заданному параметру','16.10.2021 9:59:06',9),(135,'Выполнен поиск по заданному параметру','16.10.2021 9:59:06',9),(136,'Выполнен поиск по заданному параметру','16.10.2021 9:59:06',9),(137,'Выполнен поиск по заданному параметру','16.10.2021 9:59:12',9),(138,'Выполнен поиск по заданному параметру','16.10.2021 9:59:16',9),(139,'Выполнен поиск по заданному параметру','16.10.2021 9:59:16',9),(140,'Выполнен поиск по заданному параметру','16.10.2021 9:59:17',9),(141,'Выполнен поиск по заданному параметру','16.10.2021 9:59:17',9),(142,'Выполнена сортировка по имени заказчика','16.10.2021 9:59:20',9),(143,'Выполнен поиск по заданному параметру','16.10.2021 9:59:22',9),(144,'Выполнен поиск по заданному параметру','16.10.2021 9:59:22',9),(145,'Выполнен поиск по заданному параметру','16.10.2021 9:59:22',9),(146,'Выполнена сортировка по имени заказчика','16.10.2021 9:59:24',9),(147,'Пользователь вошел в аккаунт','16.10.2021 10:33:02',9),(148,'Выполнен поиск по заданному параметру','16.10.2021 10:33:07',9),(149,'Пользователь вошел в аккаунт','16.10.2021 12:27:44',9),(150,'Выведен журнал действий','16.10.2021 12:28:16',9),(151,'Пользователь вошел в аккаунт','16.10.2021 12:52:55',7),(152,'Добавлен заказ','16.10.2021 12:54:52',7),(153,'Добавлен заказ','16.10.2021 12:55:15',7),(154,'Пользователь вошел в аккаунт','16.10.2021 12:56:14',9),(155,'Выведен журнал действий','16.10.2021 12:56:57',9),(156,'Выполнена сортировка по имени заказчика','16.10.2021 12:58:15',9),(157,'Выполнена сортировка по цене','16.10.2021 12:58:20',9),(158,'Выполнен поиск по заданному параметру','16.10.2021 12:58:28',9),(159,'Пользователь вошел в аккаунт','16.10.2021 13:16:27',9),(160,'Выполнен поиск по заданному параметру','16.10.2021 13:16:48',9),(161,'Пользователь вошел в аккаунт','16.10.2021 13:56:34',9),(162,'Пользователь вошел в аккаунт','16.10.2021 13:56:55',9),(163,'Пользователь вошел в аккаунт','19.10.2021 21:27:14',9),(164,'Пользователь вошел в аккаунт','19.10.2021 21:45:57',9),(165,'Пользователь вошел в аккаунт','19.10.2021 21:51:31',9),(166,'Пользователь вошел в аккаунт','19.10.2021 21:57:14',9),(167,'Пользователь вошел в аккаунт','19.10.2021 22:10:16',9),(168,'Пользователь вошел в аккаунт','19.10.2021 22:12:33',9),(169,'Добавлен заказчик','19.10.2021 22:12:50',9),(170,'Пользователь вошел в аккаунт','19.11.2021 21:15:12',9),(171,'Пользователь вошел в аккаунт','19.11.2021 21:35:56',9),(172,'Заказ удален','19.11.2021 21:35:59',9),(173,'Заказ удален','19.11.2021 21:36:06',9),(174,'Пользователь вошел в аккаунт','19.11.2021 22:01:54',7),(175,'Пользователь вошел в аккаунт','07.03.2022 17:26:00',9),(176,'Пользователь вошел в аккаунт','07.03.2022 17:26:37',1),(177,'Пользователь вошел в аккаунт','07.03.2022 17:31:09',1),(178,'Пользователь вошел в аккаунт','07.03.2022 17:31:43',9),(179,'Пользователь вошел в аккаунт','07.03.2022 17:42:09',1),(180,'Добавлен заказ','07.03.2022 17:43:11',1),(181,'Пользователь вошел в аккаунт','07.03.2022 17:43:48',9),(182,'Выведен журнал действий','07.03.2022 17:44:12',9),(183,'Заказ удален','07.03.2022 17:44:17',9);
/*!40000 ALTER TABLE `user_action` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `work_type`
--

DROP TABLE IF EXISTS `work_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `work_type` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `work_type`
--

LOCK TABLES `work_type` WRITE;
/*!40000 ALTER TABLE `work_type` DISABLE KEYS */;
INSERT INTO `work_type` VALUES (1,'Install',1000),(2,'Uninstall',1000),(3,'Change',670),(4,'Unintall',1000),(5,'Install',1000);
/*!40000 ALTER TABLE `work_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workshop`
--

DROP TABLE IF EXISTS `workshop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `workshop` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  `address` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workshop`
--

LOCK TABLES `workshop` WRITE;
/*!40000 ALTER TABLE `workshop` DISABLE KEYS */;
INSERT INTO `workshop` VALUES (1,'Lulya','Minsk'),(2,'Borto','Gomel'),(3,'Revo','Moscow'),(4,'Wersa','Vitebsk'),(5,'Luna','Minsk');
/*!40000 ALTER TABLE `workshop` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'mdisubd'
--

--
-- Dumping routines for database 'mdisubd'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-04-01 11:54:59
