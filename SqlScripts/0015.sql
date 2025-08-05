CREATE TABLE IF NOT EXISTS rating (
    mov_id INT NOT NULL,
    rev_id INT NOT NULL,
    rev_stars DECIMAL(3,1),
    num_o_ratings INT CHECK (num_o_ratings >= 0),
    PRIMARY KEY (mov_id, rev_id)
);
