-- Run in MySQL Workbench as root (open this file, then click the lightning-bolt "Execute" button).
-- Creates the database, a dedicated app user, and all tables.

CREATE DATABASE IF NOT EXISTS smart_recipe_book CHARACTER SET utf8mb4;

CREATE USER IF NOT EXISTS 'recipe'@'localhost' IDENTIFIED BY 'Recipe123';
GRANT ALL PRIVILEGES ON smart_recipe_book.* TO 'recipe'@'localhost';
FLUSH PRIVILEGES;

USE smart_recipe_book;

CREATE TABLE IF NOT EXISTS users (
  id            INT AUTO_INCREMENT PRIMARY KEY,
  full_name     VARCHAR(100) NOT NULL,
  email         VARCHAR(150) NOT NULL UNIQUE,
  password_hash VARCHAR(100) NOT NULL,
  role          VARCHAR(10)  NOT NULL DEFAULT 'USER',
  created_at    TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT chk_users_role CHECK (role IN ('USER','ADMIN'))
);

CREATE TABLE IF NOT EXISTS recipes (
  id           INT AUTO_INCREMENT PRIMARY KEY,
  owner_id     INT          NOT NULL,
  title        VARCHAR(150) NOT NULL,
  instructions TEXT         NOT NULL,
  photo_path   VARCHAR(500) NULL,
  status       VARCHAR(10)  NOT NULL DEFAULT 'DRAFT',
  created_at   TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT chk_recipes_status CHECK (status IN ('DRAFT','PENDING','PUBLISHED','REJECTED')),
  CONSTRAINT fk_recipes_owner FOREIGN KEY (owner_id) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS recipe_ingredients (
  id        INT AUTO_INCREMENT PRIMARY KEY,
  recipe_id INT         NOT NULL,
  name      VARCHAR(80) NOT NULL,
  quantity  VARCHAR(60) NOT NULL,
  CONSTRAINT fk_ing_recipe FOREIGN KEY (recipe_id) REFERENCES recipes(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS favorites (
  user_id   INT NOT NULL,
  recipe_id INT NOT NULL,
  PRIMARY KEY (user_id, recipe_id),
  CONSTRAINT fk_fav_user   FOREIGN KEY (user_id)   REFERENCES users(id)   ON DELETE CASCADE,
  CONSTRAINT fk_fav_recipe FOREIGN KEY (recipe_id) REFERENCES recipes(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS notifications (
  id         INT AUTO_INCREMENT PRIMARY KEY,
  user_id    INT          NOT NULL,
  message    VARCHAR(300) NOT NULL,
  is_read    TINYINT(1)   NOT NULL DEFAULT 0,
  created_at TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT fk_notif_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);
