-- insert data for DTOs test

INSERT INTO movie (mov_id, mov_title, mov_year, mov_time, mov_lang, mov_dt_rel, mov_rel_country)
VALUES (999, 'Test Movie Direction', 2025, 110, 'EN', '2025-08-08', 'US');

-- for CreateMovieDirectiontDto request
-- actor / dir_id

INSERT INTO director (dir_id, dir_fname, dir_lname)
VALUES (999, 'Test', 'Director');

-- for CreateMovieCastDto request
-- actor / act_id

INSERT INTO actor (act_id, act_fname, act_lname, act_gender)
VALUES (999, 'Test', 'Actor', 'M');

-- for MovieGenresDto request
-- actor / gen_id

INSERT INTO genres (gen_id, gen_title)
VALUES (999, 'Test');

-- for Rating request
-- actor / rev_id

INSERT INTO reviewer (rev_id, rev_name)
VALUES (999, 'Test');
