# Browser Select
Browser Select is a utility to dynamically select the browser you want instead of just having one default for all links. similar to the prompt in android to choose a browser when a link in a non-browser app is clicked/touched. it may not be useful for everyone but it really helps when you use multiple browsers for different things (e.g. one with proxy and one without) and open many links from other applications (e.g. Messengers).

![screenshot1](https://raw.githubusercontent.com/zumoshi/BrowserSelect/master/screenshots/photo_2016-07-11_13-44-19.png)

instead of having to copy the link , open desired (non-default) browser than pasting , all you need to do is to click on the link and this prompt will open allowing you to choose the browser you want. it automatically detects installed browsers , and has no need for administrative rights it can be installed and works in a restricted user.

![screenshot 2](https://raw.githubusercontent.com/zumoshi/BrowserSelect/master/screenshots/photo_2015-10-12_16-46-14.jpg)

you may click on the desired browser or press one of the shortcuts (its index or first letter of its name) , for example for chrome you can press 2 , g or c.
you may also press Esc (or click the X) to not open the url.

to install Download this file than set it as the default browser.

![select default browser](https://raw.githubusercontent.com/zumoshi/BrowserSelect/master/screenshots/photo_2015-10-12_16-43-08.jpg)

it has been tested on windows 7, windows 8.1 and windows 10. requires **.net framework 4**.

# Download

you can download browser select here : [Browser select v1.3.7 (205KB)](https://github.com/zumoshi/BrowserSelect/releases/download/1.3.7/BrowserSelect.exe)

[![100% safe Award from softpedia](http://s1.softpedia-static.com/_img/sp100free.png?1)](http://www.softpedia.com/get/Internet/Browsers/Browser-Select.shtml#status)


# Related links

[AlternativeTo](http://alternativeto.net/software/browser-select/)

Reviews: [DSTech](http://dipendrashekhawat.com/choose-specific-browser-every-time-you-open-a-link/)
[TrishTech](http://www.trishtech.com/2016/07/use-different-browsers-for-different-links-with-browserselect/)
[DonationCoder](http://www.donationcoder.com/forum/index.php?topic=42860.msg401447)

Download Mirrors: 
[GitHub](https://github.com/zumoshi/BrowserSelect/releases/latest)
[SoftPedia](http://www.softpedia.com/get/Internet/Browsers/Browser-Select.shtml)
[SnapFiles](http://www.snapfiles.com/get/browserselect.html)
[FindmySoft](http://browserselect.findmysoft.com/)
[browserss](http://browserss.ru/m.browser-select.php)
[ComputerBild](http://www.computerbild.de/download/BrowserSelect-15967517.html)

Note: Mirror's may have outdated versions of browserSelect. you can always download the latest version [here](https://github.com/zumoshi/BrowserSelect/releases).


# ToDo

just a list of some ideas that can be integrated into BrowserSelect.
- [x] Make Settings persist across updates
- [x] Shift-Click to open link in incognito/private mode
- [ ] Option to display running browsers only
- [ ] More Auto-Select rule options
    - [ ] based on source application
	- [ ] based on file extension
	- [ ] based on URL path
	- [ ] based on keywords
	- [ ] ignoring the URL as an option
	- [ ] custom flags to browsers as an option (e.g. incognito mode or disable CSRF)
- [ ] export/import for rules/settings
- [ ] Sorting browsers on the list
- [ ] Custom Shortcuts
- [ ] Ignoring the rules if Alt key is held down when clicking a link
- [ ] an API to invoke BrowserSelect
- [ ] Bugfix for when Browser was launched with Maximize window state (browser select will launch maximized)
- [ ] A browser extension to launch the correct browser based on the rules even if link is clicked inside a browser
- [ ] support for portable browsers (adding browsers using a browse button rather than registry)
- [ ] support for non-browser apps as an option (e.g. download managers)
- [ ] themes ? or at least an optional transparent Aero glass mode
- [ ] ability to choose custom icons for browsers
- [ ] display the unshortened version of adf.ly or goo.gl links when selecting the browser
- [ ] Localization
- [ ] handling of other link types (e.g. `mail:` in case you have both outlook and thunderbird installed [or maybe as a sister app])
- [x] update checker (not as a popup or messagebox, a tiny icon somewhere on the main form that appears when you don't have the last version)
- [ ] add file associations (e.g. .url files, or .html files)

# Changelog

v1.3.7 [16/08/17]
- Fixed issues with clipping on high dpi screens (#24)

v1.3.6 [11/06/17]
- BrowserSelect's window now shows up in the monitor with the mouse cursor instead of the default one (#22)

v1.3.5 [16/12/16]
- fixed crash on startup caused by incompatible/incomplete registry keys (issues #17,#20,#21)

v1.3.4 [02/09/16]
- fixed Always button adding rule with wrong pattern for second level domains (e.g. *.com.au for news.com.au)
- Shift Clicking on browsers now opens the url in incognito/private browsing
- added an update checker (adds a yellow "New" icon to main window to indicate a new version is available)[disabled by default]

v1.3.3 [03/08/16]
- fixed a crash on malformed (without protocol) url's
- added donate button in about page

v1.3.2 [28/07/16]
- bugfix to bring IE to foreground if it is already open

v1.3.1 [14/07/16]
- bugfix for Auto rule creation of domains with subdomains

v1.3 [11/07/16]
- Added an "Always" button under browser icons that adds a rule for *.domain.tld
- Added a help button in the main form
- made about form closable by Esc key
- added a help form for the settings page
- changed how filters are executed to allow simpler use of a match-all pattern
- added browser select to the list of options when adding rules
- added an apply button to the settings page for Rules
- polished the rule adding interface
- some code Formating/Indenting/Restructuring

v1.2.1 [14/06/16]
- bugfix for InternetExplorer to open links in a new tab instead of a new window

v1.2 [08/06/16]
- you can now add url patterns to select the browser based on url automatically.

v1.1 [18/05/16]
- added option to select browsers that are displayed on the list (and remove/hide some)

v1.0.2 [15/01/16]
- added option to set browser select as default browser in settings

v1.0.1 [27/10/15]
- added edge browser for windows 10 (it wouldn't show up due edge being a Universal App)
