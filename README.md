# LiveScoringService

#API Infor
developer.stats.com
UN: Alabama_scoreboard
PW: yV5asxCpWSq

API Key: d2gujudevqx32p8rd7atea8s


# Usage

The API key is setup specifically for college football, so you’ll want to navigate to CFB on the I/O docs page dropdown menu.
You can use I/O docs to get a sense of how the various calls work with our endpoints and parameters. We also have data dictionary PDFs located under the “documentation” section. At this link -- http://developer.stats.com/blog -- you will find tips from our team, including suggestions for how to create the signature required for authenticating calls.

To simulate the content you receive through the _LIVE.xml or _SCORES.xml, you can use either the EVENTS endpoint, or simply the SCORES endpoint.
 
The EVENTS endpoint houses all of the live content for a specific league. So it allows you to add parameters such as box=true, linescore=true, or pbp=true. The way we limit content based on a contractural agreement is with the BOX and SCORES endpoint. You can only get boxscore data and live scores through the box endpoint, and can only get live scores with the scores endpoint.

# Examples

Return all live scores through the EVENTS endpoint on a specific date - http://api.stats.com/v1/stats/football/cfb/events/?linescore=true&date=2017-10-07&accept=json

Same call through the SCORES endpoint - http://api.stats.com/v1/stats/football/cfb/scores/?date=2017-10-07&accept=json

To get live player statistics and play-by-play data, you must pass an EventId in the API call - http://api.stats.com/v1/stats/football/cfb/events/1721717?box=true&pbp=true&linescore=true&date=2017-09-30&accept=json
