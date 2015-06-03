<h1><font color='blue'>Information found by <code>CaitSith2</code></font></h1>

# Introduction #

New characters like Miis ([a collection](http://s1323.beta.photobucket.com/user/majorjazzman/library/Denpa%20Men%20QR/))



# QR Data #

Data size is constant of 106 bytes
  * Encrypted with an unknown algorithm but can be decrypted using a conversion table.
  * Each 2 bytes translates into 1 byte. Making 256 pair combinations for each byte to be encoded to.
  * On a QR Code, every byte is encrypted using a random pair of bytes.

## Data ##

The dechipred data:

| **Offset in bytes:bits** | **Size (bits)** | **Type** | **Description** |
|:-------------------------|:----------------|:---------|:----------------|
| 0x00:0                   | 32              | `uint32` | Region          |
| 0x04:0                   | 16              | `uint16` | Zeros           |
| 0x06:0                   | 16              | `byte[2]` | ID Start        |
| 0x08:0                   |  6              | byte     | Antenna Power   |
| 0x08:6                   |  5              | byte     | Stats           |
| 0x09:3                   |  5              | byte     | Color           |
| 0x0A:0                   |  5              | byte     | Head Shape      |
| 0x0A:5                   |  6              | byte     | Face Shape / Hair Style |
| 0x0B:3                   |  2              | byte     | Face Color      |
| 0x0B:5                   |  3              | byte     |                 |
| 0x0C:0                   |  5              | byte     | Hair Color      |
| 0x0C:5                   |  5              | byte     | Eyes            |
| 0x0D:2                   |  1              | byte     |                 |
| 0x0D:3                   |  4              | byte     | Nose            |
| 0x0D:7                   |  1              | byte     |                 |
| 0x0E:0                   |  6              | byte     | Mouth           |
| 0x0E:6                   |  3              | byte     | Eyebrows        |
| 0x0F:1                   |  2              | byte     |                 |
| 0x0F:3                   |  5              | byte     | Cheeks          |
| 0x10:0                   |  5              | byte     | Glasses         |
| 0x10:5                   |  3              | byte     |                 |
| 0x11:0                   |  4              | byte     | Stats Class     |
| 0x11:4                   |  4              | byte     |                 |
| 0x12:0                   |  7              | byte     | Color Class     |
| 0x12:7                   |  7              | byte     | Head Shape Class |
| 0x13:6                   |  7              | byte     | Antenna Power Class |
| 0x14:5                   |  7              | byte     | Face Shape / Hair Style Class |
| 0x15:4                   |  4              | byte     |                 |
| 0x16:0                   |  7              | byte     | Cheeks Class    |
| 0x16:7                   |  1              | byte     |                 |
| 0x17:0                   |  6              | byte     | Glass Class     |
| 0x17:6                   |  2              | byte     |                 |
| 0x18:0                   |  5              | byte     | Face Color Class |
| 0x18:5                   |  3              | byte     |                 |
| 0x19:0                   |  8              | byte     |                 |
| 0x1A:0                   | 192             | `wchar[12]` | Name (UTF-16) = 0x18 bytes |
| 0x32:0                   | 24              | `byte[3]` | Random values   |
| **0x35**                 | **424**         |          | **Summary = 53 bytes** |

## Conversion Table ##

  * We would address the table as F.
  * Would be 256x256, the rows would be the first byte and the columns would be the second byte.

todo.

# Values #

## Region ##

| **Value** | **Description** |
|:----------|:----------------|
| `['A','h','4','3']` | North America   |
| `['b','X','8','0']` | Japan           |
| `['j','3','Z','w']` | Europe          |

## ID ##

| **Value** | **Description** |
|:----------|:----------------|
| any value | ID Start (2 bytes) |

This can be any number and determins if this DenpaMan can be read by the game (if not already in the device).

A solution would be to randomly select a value before reading.

## Antenna Power ##
## Stats ##
## Color ##
## Head Shape ##
## Face Shape / Hair Style ##
## Face Color ##
## Hair Color ##
## Eyes ##

| **Value** | **Description** |
|:----------|:----------------|
| 0x00      | Eyes 0          |
| 0x01      | Eyes 1          |
| 0x02      | Eyes 2          |
| 0x03      | Eyes 3          |
| 0x04      | Eyes 4          |
| 0x05      | Eyes 5          |
| 0x06      | Eyes 6          |
| 0x07      | Eyes 7          |
| 0x08      | Eyes 8          |
| 0x09      | Eyes 9          |
| 0x0A      | Eyes 10         |
| 0x0B      | Eyes 11         |
| 0x0C      | Eyes 12         |
| 0x0D      | Eyes 13         |
| 0x0E      | Eyes 14         |
| 0x0F      | Eyes 15         |
| 0x10      | Eyes 16         |
| 0x11      | Eyes 17         |
| 0x12      | Eyes 18         |
| 0x13      | Eyes 19         |
| 0x14      | Eyes 20         |
| 0x15      | Eyes 21         |
| 0x16      | Eyes 22         |
| 0x17      | Eyes 23         |
| 0x18      | Eyes 24         |
| 0x19      | Eyes 25         |
| 0x1A      | Eyes 26         |
| 0x1B      | Eyes 27         |
| 0x1C      | Eyes 28         |
| 0x1D      | Eyes 29         |
| 0x1E      | Eyes 30         |
| 0x1F      | Eyes 31         |

## Nose ##

| **Value** | **Description** |
|:----------|:----------------|
| 0x0       | Nose 0          |
| 0x1       | Nose 1          |
| 0x2       | Nose 2          |
| 0x3       | Nose 3          |
| 0x4       | Nose 4          |
| 0x5       | Nose 5          |
| 0x6       | Nose 6          |
| 0x7       | Nose 7          |
| 0x8       | Nose 8          |
| 0x9       | Nose 9          |
| 0xA       | Nose 10         |
| 0xB       | Nose 11         |
| 0xC       | Nose 12         |
| 0xD       | Nose 13         |
| 0xE       | Nose 14         |
| 0xF       | Nose 15         |

## Mouth ##

| **Value** | **Description** |
|:----------|:----------------|
| 0x00      | Mouth 0         |
| 0x01      | Mouth 1         |
| 0x02      | Mouth 2         |
| 0x03      | Mouth 3         |
| 0x04      | Mouth 4         |
| 0x05      | Mouth 5         |
| 0x06      | Mouth 6         |
| 0x07      | Mouth 7         |
| 0x08      | Mouth 8         |
| 0x09      | Mouth 9         |
| 0x0A      | Mouth 10        |
| 0x0B      | Mouth 11        |
| 0x0C      | Mouth 12        |
| 0x0D      | Mouth 13        |
| 0x0E      | Mouth 14        |
| 0x0F      | Mouth 15        |
| 0x10      | Mouth 16        |
| 0x11      | Mouth 17        |
| 0x12      | Mouth 18        |
| 0x13      | Mouth 19        |
| 0x14      | Mouth 20        |
| 0x15      | Mouth 21        |
| 0x16      | Mouth 22        |
| 0x17      | Mouth 23        |
| 0x18      | Mouth 24        |
| 0x19      | Mouth 25        |
| 0x1A      | Mouth 26        |
| 0x1B      | Mouth 27        |
| 0x1C      | Mouth 28        |
| 0x1D      | Mouth 29        |
| 0x1E      | Mouth 30        |
| 0x1F      | Mouth 31        |

## Eyebrows ##

| **Value** | **Description** |
|:----------|:----------------|
| 0x0       | Eyebrows 0      |
| 0x1       | Eyebrows 1      |
| 0x2       | Eyebrows 2      |
| 0x3       | Eyebrows 3      |
| 0x4       | Eyebrows 4      |
| 0x5       | Eyebrows 5      |
| 0x6       | Eyebrows 6      |
| 0x7       | Eyebrows 7      |

## Cheeks ##

| **Value** | **Description** |
|:----------|:----------------|
| 0x0       | No cheeks       |
| 0x1       | Small cheeks    |
| 0x2       | Spiral cheeks   |
| 0x3       | Freckles        |
| 0x4       | Large cheeks    |
| 0x5       | Round cheeks    |
| 0x6       | Whiskers 1      |
| 0x7       | Whiskers 2      |

## Glasses ##

| **Value** | **Description** |
|:----------|:----------------|
| 0x0       | No glasses      |
| 0x1       | Green glasses   |
| 0x2       | Pink glasses    |
| 0x3       | Sunglasses      |
| 0x4       | Yellow glasses  |
| 0x5       | Orange glasses  |
| 0x6       | Hero Mask 1     |
| 0x7       | Brown glasses   |
| 0x8       | Blue glasses    |
| 0x9       | Hero Mask 2     |
| 0xA       | Normal glasses  |
| 0xB       | Hero Mask 3     |
| 0xC       | Goggles         |
| 0xD       | Bug eyes        |
| 0xE       | Alien eyes      |
| 0xF       | Heart glasses   |

## Classes ##
### Stats ###
### Color ###
### Anetanna Power ###
### Head Shape ###
### Face Shape / Hair Style ###
### Cheeks ###
### Glasses ###
### Face Color ###

## Name ##