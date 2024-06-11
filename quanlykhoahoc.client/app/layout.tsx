import "@mantine/core/styles.css";
import "react-toastify/dist/ReactToastify.css";
import "@mantine/charts/styles.css";
import "@mantine/dates/styles.css";
import '@mantine/tiptap/styles.css';
import "./app.css"
import React from "react";
import { MantineProvider, ColorSchemeScript } from "@mantine/core";
import { theme } from "../theme";

export const metadata = {
  title: process.env.NEXT_PUBLIC_WEBSITE_TITLE,
};

export default function RootLayout({ children }: { children: any }) {
  return (
    <html lang="en">
      <head>
        <ColorSchemeScript />
        <link rel="shortcut icon" href="/favicon.svg" />
        <meta
          name="viewport"
          content="minimum-scale=1, initial-scale=1, width=device-width, user-scalable=no"
        />
      </head>
      <body>
        <MantineProvider theme={theme}>{children}</MantineProvider>
      </body>
    </html>
  );
}
