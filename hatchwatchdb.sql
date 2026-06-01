-- veritabanı oluşturma
CREATE DATABASE IF NOT EXISTS hatchwatch_db;
USE hatchwatch_db;

-- kullanıcılar tablosu oluşturma
CREATE TABLE users (
    user_id 	INT PRIMARY KEY AUTO_INCREMENT,
    username 	VARCHAR(50) 	NOT NULL UNIQUE,
    email 		VARCHAR(100) 	NOT NULL UNIQUE,
    password 	VARCHAR(255) 	NOT NULL,
    created_at 	DATETIME 		NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- içerikler tablosu oluşturma
CREATE TABLE contents (
    content_id 			INT PRIMARY KEY AUTO_INCREMENT,
    title 				VARCHAR(150) NOT NULL,
    description 		TEXT,
    content_type 		VARCHAR(10) NOT NULL,
    release_year 		INT,
    duration_minutes 	INT,
    age_limit 			INT,
    average_rating 		DECIMAL(3 , 1 ) NOT NULL DEFAULT 0.0,
    created_at 			DATETIME 		NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    CONSTRAINT chk_content_type CHECK (content_type IN ('Film' , 'Dizi')),
    CONSTRAINT chk_release_year CHECK (release_year >= 1900
        AND release_year <= 2100),
    CONSTRAINT chk_duration CHECK (duration_minutes IS NULL
        OR duration_minutes > 0),
    CONSTRAINT chk_age_limit CHECK (age_limit IS NULL OR age_limit >= 0),
    CONSTRAINT chk_average_rating CHECK (average_rating >= 0
        AND average_rating <= 10)
);

-- türler tablosu oluşturma
CREATE TABLE genres (
    genre_id 		INT 			PRIMARY KEY 		AUTO_INCREMENT,
    genre_name 		VARCHAR(50) 	NOT NULL 			UNIQUE
);

-- platformlar tablosu oluşturma
CREATE TABLE platforms (
    platform_id INT PRIMARY KEY AUTO_INCREMENT,
    platform_name VARCHAR(50) NOT NULL UNIQUE
);

-- içerik türleri tablosu oluşturma
CREATE TABLE content_genres (
    content_id 		INT NOT NULL,
    genre_id 		INT NOT NULL,
    PRIMARY KEY (content_id , genre_id),
    
    
    CONSTRAINT fk_content_genres_content FOREIGN KEY (content_id)
        REFERENCES contents (content_id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_content_genres_genre FOREIGN KEY (genre_id)
        REFERENCES genres (genre_id)
        ON DELETE CASCADE ON UPDATE CASCADE
);
   
-- içerik platformları tablosu oluşturma
CREATE TABLE content_platforms (
    content_id INT NOT NULL,
    platform_id INT NOT NULL,
    PRIMARY KEY (content_id , platform_id),
    
    
    CONSTRAINT fk_content_platforms_content FOREIGN KEY (content_id)
        REFERENCES contents (content_id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_content_platforms_platform FOREIGN KEY (platform_id)
        REFERENCES platforms (platform_id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- izleme listesi tablosu oluşturma
CREATE TABLE watchlist (
    watchlist_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    content_id INT NOT NULL,
    watch_status VARCHAR(20) NOT NULL DEFAULT 'İzlenecek',
    user_rating INT NULL,
    added_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    CONSTRAINT fk_watchlist_user FOREIGN KEY (user_id)
        REFERENCES users (user_id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_watchlist_content FOREIGN KEY (content_id)
        REFERENCES contents (content_id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT uq_user_content UNIQUE (user_id , content_id),
    CONSTRAINT chk_watch_status CHECK (watch_status IN ('İzlenecek' , 'İzleniyor', 'İzlendi', 'Bırakıldı')),
    CONSTRAINT chk_user_rating CHECK (user_rating IS NULL
        OR user_rating BETWEEN 1 AND 10)
)

-- -----------------------------------------
-- saklı yordamlar

DELIMITER $$

/* Kullanıcılar */

CREATE PROCEDURE sp_users_select_all()
BEGIN
    SELECT user_id, username, email, password, created_at
    FROM users;
END $$

CREATE PROCEDURE sp_users_insert(
    IN p_username VARCHAR(50),
    IN p_email VARCHAR(100),
    IN p_password VARCHAR(255)
)
BEGIN
    INSERT INTO users(username, email, password)
    VALUES (p_username, p_email, p_password);
END $$

CREATE PROCEDURE sp_users_update(
    IN p_user_id INT,
    IN p_username VARCHAR(50),
    IN p_email VARCHAR(100),
    IN p_password VARCHAR(255)
)
BEGIN
    UPDATE users
    SET username = p_username,
        email = p_email,
        password = p_password
    WHERE user_id = p_user_id;
END $$

CREATE PROCEDURE sp_users_delete(
    IN p_user_id INT
)
BEGIN
    DELETE FROM users
    WHERE user_id = p_user_id;
END $$


/* İçerikler */

CREATE PROCEDURE sp_contents_select_all()
BEGIN
    SELECT content_id, title, description, content_type, release_year,
           duration_minutes, age_limit, average_rating, created_at
    FROM contents;
END $$

CREATE PROCEDURE sp_contents_insert(
    IN p_title VARCHAR(150),
    IN p_description TEXT,
    IN p_content_type VARCHAR(10),
    IN p_release_year INT,
    IN p_duration_minutes INT,
    IN p_age_limit INT
)
BEGIN
    INSERT INTO contents
    (title, description, content_type, release_year, duration_minutes, age_limit)
    VALUES
    (p_title, p_description, p_content_type, p_release_year, p_duration_minutes, p_age_limit);
END $$

CREATE PROCEDURE sp_contents_update(
    IN p_content_id INT,
    IN p_title VARCHAR(150),
    IN p_description TEXT,
    IN p_content_type VARCHAR(10),
    IN p_release_year INT,
    IN p_duration_minutes INT,
    IN p_age_limit INT
)
BEGIN
    UPDATE contents
    SET title = p_title,
        description = p_description,
        content_type = p_content_type,
        release_year = p_release_year,
        duration_minutes = p_duration_minutes,
        age_limit = p_age_limit
    WHERE content_id = p_content_id;
END $$

CREATE PROCEDURE sp_contents_delete(
    IN p_content_id INT
)
BEGIN
    DELETE FROM contents
    WHERE content_id = p_content_id;
END $$


/* Türler */

CREATE PROCEDURE sp_genres_select_all()
BEGIN
    SELECT genre_id, genre_name
    FROM genres;
END $$

CREATE PROCEDURE sp_genres_insert(
    IN p_genre_name VARCHAR(50)
)
BEGIN
    INSERT INTO genres(genre_name)
    VALUES (p_genre_name);
END $$

CREATE PROCEDURE sp_genres_update(
    IN p_genre_id INT,
    IN p_genre_name VARCHAR(50)
)
BEGIN
    UPDATE genres
    SET genre_name = p_genre_name
    WHERE genre_id = p_genre_id;
END $$

CREATE PROCEDURE sp_genres_delete(
    IN p_genre_id INT
)
BEGIN
    DELETE FROM genres
    WHERE genre_id = p_genre_id;
END $$


/* Platformlar */

CREATE PROCEDURE sp_platforms_select_all()
BEGIN
    SELECT platform_id, platform_name
    FROM platforms;
END $$

CREATE PROCEDURE sp_platforms_insert(
    IN p_platform_name VARCHAR(50)
)
BEGIN
    INSERT INTO platforms(platform_name)
    VALUES (p_platform_name);
END $$

CREATE PROCEDURE sp_platforms_update(
    IN p_platform_id INT,
    IN p_platform_name VARCHAR(50)
)
BEGIN
    UPDATE platforms
    SET platform_name = p_platform_name
    WHERE platform_id = p_platform_id;
END $$

CREATE PROCEDURE sp_platforms_delete(
    IN p_platform_id INT
)
BEGIN
    DELETE FROM platforms
    WHERE platform_id = p_platform_id;
END $$


/* İçerik Türleri */

CREATE PROCEDURE sp_content_genres_select_all()
BEGIN
    SELECT cg.content_id, c.title, cg.genre_id, g.genre_name
    FROM content_genres cg
    INNER JOIN contents c ON cg.content_id = c.content_id
    INNER JOIN genres g ON cg.genre_id = g.genre_id;
END $$

CREATE PROCEDURE sp_content_genres_insert(
    IN p_content_id INT,
    IN p_genre_id INT
)
BEGIN
    INSERT INTO content_genres(content_id, genre_id)
    VALUES (p_content_id, p_genre_id);
END $$

CREATE PROCEDURE sp_content_genres_update(
    IN p_old_content_id INT,
    IN p_old_genre_id INT,
    IN p_new_content_id INT,
    IN p_new_genre_id INT
)
BEGIN
    UPDATE content_genres
    SET content_id = p_new_content_id,
        genre_id = p_new_genre_id
    WHERE content_id = p_old_content_id
      AND genre_id = p_old_genre_id;
END $$

CREATE PROCEDURE sp_content_genres_delete(
    IN p_content_id INT,
    IN p_genre_id INT
)
BEGIN
    DELETE FROM content_genres
    WHERE content_id = p_content_id
      AND genre_id = p_genre_id;
END $$


/* İçerik Platformları */

CREATE PROCEDURE sp_content_platforms_select_all()
BEGIN
    SELECT cp.content_id, c.title, cp.platform_id, p.platform_name
    FROM content_platforms cp
    INNER JOIN contents c ON cp.content_id = c.content_id
    INNER JOIN platforms p ON cp.platform_id = p.platform_id;
END $$

CREATE PROCEDURE sp_content_platforms_insert(
    IN p_content_id INT,
    IN p_platform_id INT
)
BEGIN
    INSERT INTO content_platforms(content_id, platform_id)
    VALUES (p_content_id, p_platform_id);
END $$

CREATE PROCEDURE sp_content_platforms_update(
    IN p_old_content_id INT,
    IN p_old_platform_id INT,
    IN p_new_content_id INT,
    IN p_new_platform_id INT
)
BEGIN
    UPDATE content_platforms
    SET content_id = p_new_content_id,
        platform_id = p_new_platform_id
    WHERE content_id = p_old_content_id
      AND platform_id = p_old_platform_id;
END $$

CREATE PROCEDURE sp_content_platforms_delete(
    IN p_content_id INT,
    IN p_platform_id INT
)
BEGIN
    DELETE FROM content_platforms
    WHERE content_id = p_content_id
      AND platform_id = p_platform_id;
END $$


/* İzleme Listesi */

CREATE PROCEDURE sp_watchlist_select_all()
BEGIN
    SELECT w.watchlist_id,
           w.user_id,
           u.username,
           w.content_id,
           c.title,
           w.watch_status,
           w.user_rating,
           w.added_at,
           w.updated_at
    FROM watchlist w
    INNER JOIN users u ON w.user_id = u.user_id
    INNER JOIN contents c ON w.content_id = c.content_id;
END $$

CREATE PROCEDURE sp_watchlist_insert(
    IN p_user_id INT,
    IN p_content_id INT,
    IN p_watch_status VARCHAR(20),
    IN p_user_rating INT
)
BEGIN
    INSERT INTO watchlist(user_id, content_id, watch_status, user_rating)
    VALUES (p_user_id, p_content_id, p_watch_status, p_user_rating);
END $$

CREATE PROCEDURE sp_watchlist_update(
    IN p_watchlist_id INT,
    IN p_watch_status VARCHAR(20),
    IN p_user_rating INT
)
BEGIN
    UPDATE watchlist
    SET watch_status = p_watch_status,
        user_rating = p_user_rating,
        updated_at = CURRENT_TIMESTAMP
    WHERE watchlist_id = p_watchlist_id;
END $$

CREATE PROCEDURE sp_watchlist_delete(
    IN p_watchlist_id INT
)
BEGIN
    DELETE FROM watchlist
    WHERE watchlist_id = p_watchlist_id;
END $$

DELIMITER ;

CALL sp_users_insert('musti', 'musti@mail.com', '123456');
CALL sp_users_insert('bubi', 'bubi@mail.com', '654321');
CALL sp_users_select_all();
-- -----------------------------
-- fonksiyonlar
DELIMITER $$

CREATE FUNCTION fn_user_watched_count(p_user_id INT)
RETURNS INT
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE watched_count INT;

    SELECT COUNT(*)
    INTO watched_count
    FROM watchlist
    WHERE user_id = p_user_id
      AND watch_status = 'İzlendi';

    RETURN watched_count;
END $$

CREATE FUNCTION fn_content_average_rating(p_content_id INT)
RETURNS DECIMAL(3,1)
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE avg_rating DECIMAL(3,1);

    SELECT IFNULL(AVG(user_rating), 0)
    INTO avg_rating
    FROM watchlist
    WHERE content_id = p_content_id
      AND user_rating IS NOT NULL;

    RETURN avg_rating;
END $$

	

SELECT fn_user_watched_count(1) AS izlenen_icerik_sayisi;

SELECT fn_content_average_rating(1) AS ortalama_puan;


-- -----------------------------
-- triggerlar

DELIMITER $$
-- Kullanıcı bir içeriği “İzlendi” yapıyorsa puan girmesini zorunlu tutar.
CREATE TRIGGER trg_watchlist_rating_required
BEFORE INSERT ON watchlist
FOR EACH ROW
BEGIN
    IF NEW.watch_status = 'İzlendi' AND NEW.user_rating IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'İzlendi olarak işaretlenen içerik için puan girilmelidir.';
    END IF;
END $$

-- İzleme listesinde güncelleme yapılınca updated_at alanını otomatik günceller.
CREATE TRIGGER trg_watchlist_update_date
BEFORE UPDATE ON watchlist
FOR EACH ROW
BEGIN
    SET NEW.updated_at = CURRENT_TIMESTAMP;
END $$

DELIMITER ;