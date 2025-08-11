# ===== MOVIES =====
curl -i -X OPTIONS "http://localhost:5000/api/Movies" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Movies/929" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Movies" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/Movies/929" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: PATCH"
curl -i -X OPTIONS "http://localhost:5000/api/Movies/929" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== GENRES =====
curl -i -X OPTIONS "http://localhost:5000/api/Genres" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Genres/1014" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Genres" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/Genres/1014" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: PATCH"
curl -i -X OPTIONS "http://localhost:5000/api/Genres/1014" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== MOVIEGENRES =====
curl -i -X OPTIONS "http://localhost:5000/api/MovieGenres" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/MovieGenres/929/1014" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/MovieGenres" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/MovieGenres/929/1014" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== ACTORS =====
curl -i -X OPTIONS "http://localhost:5000/api/Actors" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Actors/125" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Actors" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/Actors/125" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: PATCH"
curl -i -X OPTIONS "http://localhost:5000/api/Actors/125" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== MOVIECASTS =====
curl -i -X OPTIONS "http://localhost:5000/api/MovieCasts" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/MovieCasts/125/929" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/MovieCasts" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/MovieCasts/125/929" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== REVIEWERS =====
curl -i -X OPTIONS "http://localhost:5000/api/Reviewers" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Reviewers/9021" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Reviewers" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/Reviewers/9021" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: PATCH"
curl -i -X OPTIONS "http://localhost:5000/api/Reviewers/9021" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== RATINGS =====
curl -i -X OPTIONS "http://localhost:5000/api/Ratings" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Ratings/929/9021" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Ratings" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/Ratings/929/9021" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== DIRECTORS =====
curl -i -X OPTIONS "http://localhost:5000/api/Directors" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Directors/224" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/Directors" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/Directors/224" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: PATCH"
curl -i -X OPTIONS "http://localhost:5000/api/Directors/224" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"

# ===== MOVIEDIRECTION =====
curl -i -X OPTIONS "http://localhost:5000/api/MovieDirection" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/MovieDirection/224/929" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: GET"
curl -i -X OPTIONS "http://localhost:5000/api/MovieDirection" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: POST"
curl -i -X OPTIONS "http://localhost:5000/api/MovieDirection/224/929" \
  -H "Origin: $ORIGIN" -H "Access-Control-Request-Method: DELETE"
