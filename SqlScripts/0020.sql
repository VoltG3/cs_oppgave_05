
-- for CreateMovieCastDto request
-- movie / mov_id
-- actor / act_id

INSERT INTO movie (mov_id, mov_title, mov_year, mov_time, mov_lang, mov_dt_rel, mov_rel_country) 
VALUES (999, 'Test Movie', 2025, 120, 'EN', '2025-01-01', 'US');

INSERT INTO actor (act_id, act_fname, act_lname, act_gender) 
VALUES (999, 'Test', 'Actor', 'M');