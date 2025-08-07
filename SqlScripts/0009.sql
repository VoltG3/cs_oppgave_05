CREATE TABLE movie (
    mov_id INT PRIMARY KEY,
    mov_title VARCHAR(50) NOT NULL,
    mov_year INT,
    mov_time INT CHECK (mov_time > 0),
    mov_lang VARCHAR(50),
    mov_dt_rel DATE,
    mov_rel_country VARCHAR(5)
);
