CREATE TABLE IF NOT EXISTS movie_direction (
    dir_id INT NOT NULL,
    mov_id INT NOT NULL,
    PRIMARY KEY (dir_id, mov_id)
);
