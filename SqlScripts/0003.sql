CREATE TABLE IF NOT EXISTS movie_cast (
    act_id INT NOT NULL,
    mov_id INT NOT NULL,
    role VARCHAR(30),
    PRIMARY KEY (act_id, mov_id)
);
