﻿namespace MusicApp.Contracts;

public class SongContract
{
    public string Id { get; set; }

    public string Name { get; set; }

    public int Number {  get; set; }

    public int DurationInSeconds { get; set; }

    public List<string> FeaturingArtistIds {  get; set; }

    public string AlbumId { get; set; }
}

public class SongCreateAndUpdateContract
{
    public string Name { get; set; }
    public int Number { get; set; }
    public int DurationInSeconds { get; set; }
    public List<string> FeaturingArtistIds { get; set; }
}
