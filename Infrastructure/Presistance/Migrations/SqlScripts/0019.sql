-- 0011.sql - Source table     - genres
-- 0009.sql - Source table     - movie
-- 0013.sql - Receiver table   - movie_genres

ALTER TABLE movie_genres
    ADD CONSTRAINT fk_moviegenres_genid
        FOREIGN KEY (gen_id)
        REFERENCES genres(gen_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;

ALTER TABLE movie_genres
    ADD CONSTRAINT fk_moviegenres_movid
        FOREIGN KEY (mov_id)
        REFERENCES movie(mov_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;

-- 0009.sql - Source table     - movie
-- 0017.sql - Source table     - reviewer
-- 0015.sql - Receiver table   - rating

ALTER TABLE rating
    ADD CONSTRAINT fk_rating_movid
        FOREIGN KEY (mov_id)
        REFERENCES movie(mov_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;

ALTER TABLE rating
    ADD CONSTRAINT fk_rating_revid
        FOREIGN KEY (rev_id)
        REFERENCES reviewer(rev_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;

-- 0001.sql - Source table     - actor 
-- 0009.sql - Source table     - movie
-- 0003.sql - Receiver table   - movie_cast

ALTER TABLE movie_cast
    ADD CONSTRAINT fk_moviecast_actid
        FOREIGN KEY (act_id)
        REFERENCES actor(act_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;

ALTER TABLE movie_cast
    ADD CONSTRAINT fk_movie_movid
        FOREIGN KEY (mov_id)
        REFERENCES movie(mov_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;

-- 0009.sql - Source table     - movie
-- 0007.sql - Source table     - director
-- 0005.sql - Receiver table   - movie_direction

ALTER TABLE movie_direction
    ADD CONSTRAINT fk_moviedirection_movid
        FOREIGN KEY (mov_id)
            REFERENCES movie(mov_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;

ALTER TABLE movie_direction
    ADD CONSTRAINT fk_moviedirection_dirid
        FOREIGN KEY (dir_id)
            REFERENCES director(dir_id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;
