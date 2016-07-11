**Pattern**: what URLs match the rule. you may use:
- a domain name (a URL without the path e.g. `google.com , www.mywebsite.org`)
- a wildcard pattern ( `*.local , *.us , *.google.com , website.*` )
- an asterisk ( `*` ) which will match everything

**Browser**: what happens when the pattern matches. which is either a browser to be opened automatically or "display browser select" to exclude a pattern from auto-selection.

*Note*: you can open BrowserSelect manually from startmenu to change the Filters. BrowserSelect always opens itself when launched from startmenu regardless of Filters added.


Examples
---

1. open all websites in firefox but companywebsite.com in IE:
    - rule 1: `*` -> `Firefox`
    - rule 2: `*.companywebsite.com` -> `IE`
2. open google websites (`mail.google.com, contacts.google.com, ...`) in chrome, github in firefox and ask for other website:
    - rule 1: `*.google.com` -> `chrome`
    - rule 2: `github.com` -> `firefox`
    note: you may add a third rule with `*` for display browser select but it is not nessesary, Browser select defaults to asking when no rule matches
3. ask which browser for `.us` domains, open all other websites with firefox:
    - rule 1: `*.us` -> `display browser select`
    - rule 2: `*` -> `firefox`