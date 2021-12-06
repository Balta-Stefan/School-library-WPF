-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Users` (
  `userID` INT NOT NULL AUTO_INCREMENT,
  `firstName` VARCHAR(45) NOT NULL,
  `lastName` VARCHAR(45) NOT NULL,
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(128) NOT NULL,
  `userType` VARCHAR(25) NOT NULL,
  `active` TINYINT NOT NULL DEFAULT 1,
  `localization` VARCHAR(15) NULL,
  `theme` VARCHAR(10) NULL,
  PRIMARY KEY (`userID`),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Accountants`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Accountants` (
  `userID` INT NOT NULL,
  PRIMARY KEY (`userID`),
  CONSTRAINT `Accountants_userID_FK`
    FOREIGN KEY (`userID`)
    REFERENCES `mydb`.`Users` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Librarians`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Librarians` (
  `userID` INT NOT NULL,
  PRIMARY KEY (`userID`),
  CONSTRAINT `Librarians_userID_FK`
    FOREIGN KEY (`userID`)
    REFERENCES `mydb`.`Users` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Members`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Members` (
  `userID` INT NOT NULL,
  PRIMARY KEY (`userID`),
  CONSTRAINT `Members_userID_FK`
    FOREIGN KEY (`userID`)
    REFERENCES `mydb`.`Users` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Authors`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Authors` (
  `authorID` INT NOT NULL AUTO_INCREMENT,
  `firstName` VARCHAR(45) NOT NULL,
  `lastName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`authorID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Publishers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Publishers` (
  `publisherID` INT NOT NULL AUTO_INCREMENT,
  `publisherName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`publisherID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Genres`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Genres` (
  `genreID` INT NOT NULL AUTO_INCREMENT,
  `genreName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`genreID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Books`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Books` (
  `bookID` INT NOT NULL AUTO_INCREMENT,
  `ISBN13` CHAR(13) NULL,
  `ISBN10` CHAR(10) NULL,
  `bookTitle` VARCHAR(45) NOT NULL,
  `edition` SMALLINT NULL,
  `authorID` INT NOT NULL,
  `publisherID` INT NOT NULL,
  `genre` INT NOT NULL,
  `numberOfCopies` INT NULL DEFAULT 0,
  PRIMARY KEY (`bookID`),
  INDEX `authorID_idx` (`authorID` ASC) VISIBLE,
  INDEX `publisherID_idx` (`publisherID` ASC) VISIBLE,
  INDEX `genre_idx` (`genre` ASC) VISIBLE,
  UNIQUE INDEX `ISBN13_UNIQUE` (`ISBN13` ASC) VISIBLE,
  UNIQUE INDEX `ISBN10_UNIQUE` (`ISBN10` ASC) VISIBLE,
  CONSTRAINT `authorID`
    FOREIGN KEY (`authorID`)
    REFERENCES `mydb`.`Authors` (`authorID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `publisherID`
    FOREIGN KEY (`publisherID`)
    REFERENCES `mydb`.`Publishers` (`publisherID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `genre`
    FOREIGN KEY (`genre`)
    REFERENCES `mydb`.`Genres` (`genreID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Book_conditions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Book_conditions` (
  `conditionID` INT NOT NULL AUTO_INCREMENT,
  `condition` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`conditionID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Book_copies`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Book_copies` (
  `bookCopyID` INT NOT NULL AUTO_INCREMENT,
  `conditionID` INT NOT NULL,
  `deliveredAt` DATETIME NULL,
  `bookID` INT NOT NULL,
  `available` TINYINT NOT NULL DEFAULT 1,
  PRIMARY KEY (`bookCopyID`),
  INDEX `bookID_idx` (`bookID` ASC) VISIBLE,
  INDEX `conditionID_idx` (`conditionID` ASC) VISIBLE,
  CONSTRAINT `bookID`
    FOREIGN KEY (`bookID`)
    REFERENCES `mydb`.`Books` (`bookID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `conditionID`
    FOREIGN KEY (`conditionID`)
    REFERENCES `mydb`.`Book_conditions` (`conditionID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Loans`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Loans` (
  `loanID` INT NOT NULL AUTO_INCREMENT,
  `borrowDateTime` DATETIME NOT NULL,
  `borrowedFromLibrarian` INT NOT NULL,
  `borrowerID` INT NOT NULL,
  `bookCopyID` INT NOT NULL,
  `returnedToLibrarian` INT NULL,
  `returnDateTime` DATETIME NULL,
  PRIMARY KEY (`loanID`),
  INDEX `borrowedFromLibrarian_idx` (`borrowedFromLibrarian` ASC) VISIBLE,
  INDEX `borrowerID_idx` (`borrowerID` ASC) VISIBLE,
  INDEX `bookCopyID_idx` (`bookCopyID` ASC) VISIBLE,
  INDEX `returnedToLibrarian_idx` (`returnedToLibrarian` ASC) VISIBLE,
  CONSTRAINT `borrowedFromLibrarian`
    FOREIGN KEY (`borrowedFromLibrarian`)
    REFERENCES `mydb`.`Librarians` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `borrowerID`
    FOREIGN KEY (`borrowerID`)
    REFERENCES `mydb`.`Members` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `bookCopyID`
    FOREIGN KEY (`bookCopyID`)
    REFERENCES `mydb`.`Book_copies` (`bookCopyID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `returnedToLibrarian`
    FOREIGN KEY (`returnedToLibrarian`)
    REFERENCES `mydb`.`Librarians` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `mydb`;

DELIMITER $$
USE `mydb`$$
CREATE DEFINER = CURRENT_USER TRIGGER `mydb`.`Book_copies_AFTER_INSERT` AFTER INSERT ON `Book_copies` FOR EACH ROW
BEGIN
	UPDATE Books SET numberOfCopies=numberOfCopies+1 WHERE bookID=NEW.bookID;
END$$

USE `mydb`$$
CREATE DEFINER = CURRENT_USER TRIGGER `mydb`.`Book_copies_BEFORE_DELETE` BEFORE DELETE ON `Book_copies` FOR EACH ROW
BEGIN
	UPDATE Books SET numberOfCopies=numberOfCopies-1 WHERE bookID=OLD.bookID;
END$$

USE `mydb`$$
CREATE DEFINER = CURRENT_USER TRIGGER `mydb`.`Loans_AFTER_INSERT` AFTER INSERT ON `Loans` FOR EACH ROW
BEGIN
	UPDATE Book_copies bc SET available=FALSE WHERE bc.bookCopyID=NEW.bookCopyID;
END$$

USE `mydb`$$
CREATE DEFINER = CURRENT_USER TRIGGER `mydb`.`Loans_BEFORE_UPDATE` BEFORE UPDATE ON `Loans` FOR EACH ROW
BEGIN
	IF NEW.returnedToLibrarian IS NOT NULL THEN 
		UPDATE Book_copies bc SET available=TRUE WHERE bc.bookCopyID = NEW.bookCopyID;
    END IF;
END$$


DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
