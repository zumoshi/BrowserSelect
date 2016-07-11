# Browser Select
Browser Select is a utility to dynamically select the browser you want instead of just having one default for all links. similar to the prompt in android to choose a browser when a link in a non-browser app is clicked/touched. it may not be useful for everyone but it really helps when you use multiple browsers for different things (e.g. one with proxy and one without) and open many links from other applications (e.g. Messengers).

![screenshot1](https://raw.githubusercontent.com/zumoshi/BrowserSelect/master/screenshots/photo_2016-07-11 13_44_19.png)

instead of having to copy the link , open desired (non-default) browser than pasting , all you need to do is to click on the link and this prompt will open allowing you to choose the browser you want. it automatically detects installed browsers , and has no need for administrative rights it can be installed and works in a restricted user.

![screenshot 2](https://raw.githubusercontent.com/zumoshi/BrowserSelect/master/screenshots/photo_2015-10-12_16-46-14.jpg)

you may click on the desired browser or press one of the shortcuts (its index or first letter of its name) , for example for chrome you can press 2 , g or c.
you may also press Esc (or click the X) to not open the url.

to install Download this file than set it as the default browser.

![select default browser](https://raw.githubusercontent.com/zumoshi/BrowserSelect/master/screenshots/photo_2015-10-12_16-43-08.jpg)

it has been tested on windows 7, windows 8.1 and windows 10. requires **.net framework 4**.

# Download

you can download browser select here : [Browser select v1.3 (197KB)](https://github.com/zumoshi/BrowserSelect/releases/download/1.3.0/BrowserSelect.exe)



# Changelog 

v1.3
- Added an "Always" button under browser icons that adds a rule for *.domain.tld
- Added a help button in the main form
- made about form closable by Esc key
- added a help form for the settings page
- changed how filters are executed to allow simpler use of a match-all pattern
- added browser select to the list of options when adding rules
- added an apply button to the settings page for Rules
- polished the rule adding interface
- some code Formating/Indenting/Restructuring

v1.2.1
- bugfix for InternetExplorer to open links in a new tab instead of a new window

v1.2
- you can now add url patterns to select the browser based on url automatically.

v1.1
- added option to select browsers that are displayed on the list (and remove/hide some)

v1.0.2
- added option to set browser select as default browser in settings

v1.0.1
- added edge browser for windows 10 (it wouldn't show up due edge being a Universal App)