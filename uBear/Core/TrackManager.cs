﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCNLDrivers;
using UCNLKML;
using UCNLNav;

namespace uBear.Core
{
    public class TrackManager
    {
        #region Properties

        Dictionary<string, List<GeoPoint3DTm>> tracks;

        public bool IsEmpty
        {
            get { return tracks.Count == 0; }
        }

        #endregion

        #region Constructor

        public TrackManager()
        {
            tracks = new Dictionary<string, List<GeoPoint3DTm>>();
        }

        #endregion

        #region Methods

        public void Clear()
        {
            tracks.Clear();
            IsEmptyChanged.Rise(this, new EventArgs());
        }

        public void AddPoint(string trackID, double lat, double lon, double dpt, DateTime timeStamp)
        {
            bool prevIsEmpty = IsEmpty;

            if (!tracks.ContainsKey(trackID))
                tracks.Add(trackID, new List<GeoPoint3DTm>());

            tracks[trackID].Add(new GeoPoint3DTm(lat, lon, dpt, timeStamp));

            if (!prevIsEmpty)
                IsEmptyChanged.Rise(this, new EventArgs());
        }

        public void ExportToKML(string fileName)
        {
            KMLData data = new KMLData(fileName, "Generated by uBear application");
            List<KMLLocation> kmlTrack;

            foreach (var item in tracks)
            {
                kmlTrack = new List<KMLLocation>();
                foreach (var point in item.Value)
                    kmlTrack.Add(new KMLLocation(point.Longitude, point.Latitude, -point.Depth));

                data.Add(new KMLPlacemark(string.Format("{0} track", item.Key), "", kmlTrack.ToArray()));
            }

            TinyKML.Write(data, fileName);
        }

        public void ExportToCSV(string fileName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var track in tracks)
            {
                sb.AppendFormat("\r\nTrack name: {0}\r\n", track.Key);
                sb.Append("HH:MM:SS;LAT;LON;DPT;\r\n");

                foreach (var point in track.Value)
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        "{0:00};{1:00};{2:00};{3:F06};{4:F06};{5:F03}\r\n",
                        point.TimeStamp.Hour,
                        point.TimeStamp.Minute,
                        point.TimeStamp.Second,
                        point.Latitude,
                        point.Longitude,
                        point.Depth);
                }
            }

            File.WriteAllText(fileName, sb.ToString());
        }

        #endregion

        #region Events

        public EventHandler IsEmptyChanged;

        #endregion
    }
}
