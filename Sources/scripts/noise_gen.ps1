
$magick = "C:\dev\portable\ImageMagick-7.0.9-5-portable-Q16-x64\convert.exe"

#$noise_type = "random"
$noise_type = "gaussian"
#$noise_type = "impulse"

# 16:9
$aspect_ratio = 16 / 9;
$width = 800
$height = [int]($width / $aspect_ratio)

$resolution = [string]$width + "x" + [string]$height

$fps = 24
$len_seconds = 5

$frames = $fps * $len_seconds
$left_pad = ([string]$frames).Length

for ( $i = 0; $i -lt $frames; $i++ ){
    [string]$seq = ([string]$i).PadLeft($left_pad, '0')
    $seed = $i * 17
    $command = "$magick -size $resolution xc:gray +noise $noise_type -seed $seed -colorspace gray noise_$seq.png"
    Write-Host $command
    Invoke-Expression $command
}

# generazione video:
Write-Host "ffmpeg -framerate $fps -pattern_type sequence -start_number 0 -i noise_%02d.png -b:v 20M noise.wmv"
