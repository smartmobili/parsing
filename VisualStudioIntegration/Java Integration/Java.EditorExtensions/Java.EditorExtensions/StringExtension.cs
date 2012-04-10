﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;

namespace Java.EditorExtensions
{
    /// <summary>
    /// Provides extensions for <see cref="String"/>
    /// </summary>
    internal static class StringExtension
    {
        /// <summary>
        /// Returns index of the last token
        /// </summary>
        /// <param name="textBuffer"></param>
        /// <returns></returns>
        internal static int GetCommentStopIndex(this String text, int startIndex)
        {
            int stopIndex = startIndex;

            while (stopIndex < text.Length - 1)
            {
                char curChar = text.ElementAt(stopIndex);
                if (curChar == '\r' || curChar == '\n')
                {
                    stopIndex -= 2;
                    break;
                }

                stopIndex++;
            }

            return stopIndex;
        }
    }
}