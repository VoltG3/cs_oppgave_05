echo ""
echo "################################################"
echo "#      CURL TEST 1 - MASTER TABLE - MOVIE      #"
echo "################################################"
echo ""

echo ""
echo "POST [ movie ]"
echo ""
curl -X POST "http://localhost:5000/api/Movies" \
  -H "Content-Type: application/json" \
  -d '{"movTitle":"Movie_929","movYear":2025,"movTime":120,"movLang":"EN","movDtRel":"2025-08-08","movRelCountry":"US"}'
echo ""
  
echo ""
echo "PACH [ movie ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Movies/929" \
  -H "Content-Type: application/json" \
  -d '{"movTitle":"Movie_PACHED_929"}'
echo ""

echo ""
echo "GET [ movie ]"
echo ""
curl -X GET "http://localhost:5000/api/Movies/929" \
  -H "Accept: application/json"
echo ""

echo ""
echo "###############################################"
echo "#      CURL TEST 2 - SLAVE TABLE - GENRE      #"
echo "###############################################"
echo ""

echo ""
echo "POST [ genre ]"
echo ""
curl -X POST "http://localhost:5000/api/Genres" \
  -H "Content-Type: application/json" \
  -d '{"genTitle":"Genre_929"}'
echo ""

echo ""
echo "PATCH [ genre ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Genres/1014" \
  -H "Content-Type: application/json" \
  -d '{"genTitle":"Genre_PACHED_929"}'
echo ""

echo ""
echo "GET [ genre ]"
echo ""
curl -X GET "http://localhost:5000/api/Genres/1014" \
  -H "Accept: application/json"
echo ""

echo ""  
echo "###################################################################################"
echo "#      CURL TEST 2 - RELATIONS - [ movie ] >> [ movie_genres ] << [ genres ]      #"
echo "###################################################################################"
echo ""

echo ""
echo "POST [ movie_genres ]"
echo ""
curl -X POST "http://localhost:5000/api/MovieGenres" \
  -H "Content-Type: application/json" \
  -d '{"movId":929,"genId":1014}'
echo ""

echo ""
echo "GET [ movie_genres: movie & genres ]"
echo ""
curl -X GET "http://localhost:5000/api/MovieGenres?movId=929&genId=1014" -H "Accept: application/json"

echo ""
echo "###############################################"
echo "#      CURL TEST 3 - SLAVE TABLE - ACTOR      #"
echo "###############################################"
echo ""

echo ""
echo "POST [ actor ]"
echo ""
curl -X POST "http://localhost:5000/api/Actors" \
  -H "Content-Type: application/json" \
  -d '{"actFname":"ActorFname_929","actLname":"ActorLname_929","actGender":"M"}'
echo ""

echo ""
echo "PATCH [ actor ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Actors/125" \
  -H "Content-Type: application/json" \
  -d '{"actFname":"ActorFname_PATCH_929"}'

echo ""
echo "GET [ actor ]"
echo ""
curl -X GET "http://localhost:5000/api/Actors/125" \
  -H "Accept: application/json"
echo ""

echo ""
echo "################################################################################"
echo "#      CURL TEST 3 - RELATIONS - [ movie ] >> [ movie_cast ] << [ actor ]      #"
echo "################################################################################"
echo ""

echo ""
echo "POST [ movie_cast ]"
echo ""
curl -X POST "http://localhost:5000/api/MovieCasts" \
  -H "Content-Type: application/json" \
  -d '{"actId":125,"movId":929,"role":"Role_929"}'
echo ""

echo ""
echo "GET [ movie_cast: mmovie & actor ]"
echo ""
curl -X GET "http://localhost:5000/api/MovieCasts?actId=125&movId=929" -H "Accept: application/json"
echo ""

echo ""
echo "##################################################"
echo "#      CURL TEST 4 - SLAVE TABLE - REVIEWER      #"
echo "##################################################"
echo ""

echo ""
echo "POST [ reviewer ]"
echo ""
curl -X POST "http://localhost:5000/api/Reviewers" \
  -H "Content-Type: application/json" \
  -d '{"revName":"revName_929"}'
echo ""

echo ""
echo "PATCH [ reviewer ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Reviewers/9021" \
  -H "Content-Type: application/json" \
  -d '{"revName":"revName_PATCHED_929"}'
echo ""

echo ""
echo "GET [ reviewer ]"
echo ""
curl -X GET "http://localhost:5000/api/Reviewers/9021" \
  -H "Accept: application/json"
echo ""

echo ""
echo "###############################################################################"
echo "#      CURL TEST 4 - RELATIONS - [ movie ] >> [ rating ] << [ reviewer ]      #"
echo "###############################################################################"
echo ""

echo ""
echo "POST [ reviewer ]"
echo ""
curl -X POST "http://localhost:5000/api/Ratings" \
  -H "Content-Type: application/json" \
  -d '{"movId":929,"revId":9021,"revStars":4.8,"numOfRatings":120}'
echo ""

echo ""
echo "GET [ rating: movie & reviewer ]"
echo ""
curl -X GET "http://localhost:5000/api/Ratings?movId=929&revId=9021" -H "Accept: application/json"
echo ""

echo ""
echo "##################################################"
echo "#      CURL TEST 5 - SLAVE TABLE - DIRECTOR      #"
echo "##################################################"
echo ""

echo ""
echo "POST [ director ]"
echo ""
curl -X POST "http://localhost:5000/api/Directors" \
  -H "Content-Type: application/json" \
  -d '{"dirFname":"dirFname_929", "dirLname":"dirLname_929"}'
echo ""

echo ""
echo "PATCH [ director ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Directors/224" \
  -H "Content-Type: application/json" \
  -d '{"dirFname":"dirFname_PATCHED_929"}'
echo ""

echo ""
echo "GET [ director ]"
echo ""
curl -X GET "http://localhost:5000/api/Directors/224" \
  -H "Accept: application/json"
echo ""

echo ""
echo "########################################################################################"
echo "#      CURL TEST 5 - RELATIONS - [ movie ] >> [ movie_direction ] << [ director ]      #"
echo "########################################################################################"
echo ""

echo ""
echo "POST [ movie_genres ]"
echo ""
curl -X POST "http://localhost:5000/api/MovieDirection" \
  -H "Content-Type: application/json" \
  -d '{"dirId":224,"movId":929}'
echo ""

echo ""
echo "GET [ movie_directions: movie-director ]"
echo ""
curl -X GET "http://localhost:5000/api/MovieDirection?dirId=224&movId=929" -H "Accept: application/json"
echo ""