﻿﻿//
// Copyright (c) Antmicro
//
// This file is part of the Emul8 project.
// Full license details are defined in the 'LICENSE' file.
//
using Emul8.UserInterface;
using TermSharp;
using TermSharp.Rows;
using Xwt;
using Xwt.Drawing;

namespace Emul8.CLI
{
    public class LogoRow : MonospaceTextRow
    {
        public LogoRow() : base("")
        {
            image = Image.FromResource("logo.png");
        }

        public override double PrepareForDrawing(ILayoutParameters parameters)
        {
            var baseResult = base.PrepareForDrawing(parameters);
            if(LineHeight == 0) // UI has not been initalized yet
            {
                return baseResult;
            }
            targetImageHeight = PreferedHeightInLines * LineHeight;
            ShellProvider.NumberOfDummyLines = PreferedHeightInLines;
            return baseResult;
        }

        public override void Draw(Context ctx, Rectangle selectedArea, SelectionDirection selectionDirection, TermSharp.SelectionMode selectionMode)
        {
            var scale = targetImageHeight / image.Height;
            ctx.Scale(scale, scale);
            ctx.DrawImage(image, new Point());
            ctx.Translate(0, -targetImageHeight);
            base.Draw(ctx, selectedArea, selectionDirection, selectionMode);
        }

        private double targetImageHeight;
        private readonly Image image;
        private const int PreferedHeightInLines = 3;
    }
}
