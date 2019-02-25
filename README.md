# event-buddy
Internal tool to ingest event sign-ins and automatically add them to various ACM mailing lists.


To Run:

1) Open .sln file in Visual Studio ORRRR see step 2

2) Click Play at the top    ORRRRR use the .exe file in Debug/bin folder I pushed.

3) A Command window will open asking for a .csv path -- this is the spreadsheet downloaded from an ACM event. Paste it (ie D:\Downloads\event1.csv)

4) It will parse the student data -- if they are missing a last name or something it will say "failed to parse"

5) It will ask you to run Selenium in either Chrome or Firefox (I only tested in Chrome).

6) It will bring you to a google sign in page. Sign in and then click back to the command window and press any key.

7) It will take you to each invite page 1 by 1 and fill in the corresponding emails then click submit. You will need to verify any bad information or do an "I am not a robot" challenge. Then return to the command window and press any key.

8) Groups that I did not have access to I just wrote to a csv file.

