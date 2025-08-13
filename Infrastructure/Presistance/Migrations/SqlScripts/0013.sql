CREATE TABLE IF NOT EXISTS movie_genres (
    mov_id INT NOT NULL,
    gen_id INT NOT NULL,
    PRIMARY KEY (mov_id, gen_id)
);
