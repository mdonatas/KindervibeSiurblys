## Kindervibe Siurblys (Kindervibe Vacuum)
This is a simple command line tool for downloading photos from the Kindervibe app.

Since Kindervibe user experience is not the best when it comes to making photo backups it seemed that a tool such as this one would be beneficial to at least some of the users.

### Features
- Supports accounts with several children
- Makes a backup for a range of dates. E.g. `2023-01-01` - `2023-04-01`
- Photos from Kindervibe come without an EXIF dates, meaning there is no **Date** information on the photo file. Kindervibe Siurblys sets the correct date, albeit not the correct time as that information is not available
- Video files are also downloaded although no `Date` information is set on them
- Photo and Video files are placed in a `Kindervibe` directory created next to the app

### Demo
![](gh-pages/kindervibe-siurblys.gif)

### Download
Application files for various platforms are located at the [releases section](https://github.com/mdonatas/KindervibeSiurblys/releases)

#### A note for OSX (Mac) and Linux users
To make the app file executable please follow these instruction in the Terminal app
```shell
mdonatas@macbook ~ % cd ~/Downloads
mdonatas@macbook Downloads % chmod +x KindervibeSiurblys-osx
mdonatas@macbook Downloads % ./KindervibeSiurblys-osx
Enter your Kindervibe username (probably your email):
```
- `cd ~/Downloads` navigates to the `Downloads` directory assuming that's where the app was downloaded
- `chmod +x KindervibeSiurblys-osx` makes the file executable, without this the app file cannot run
- `./KindervibeSiurblys-osx` starts the app
