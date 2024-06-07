"use client";

import Editor from "@monaco-editor/react";
import RootLayout from "../../components/Layout/RootLayout";
import { Paper } from "@mantine/core";

const defaultValue = `
using System;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine ("Try programiz.pro");
    }
}
`;

export default function Detail() {
  return (
    <RootLayout>
      <Paper withBorder>
        <Editor
          height="90vh"
          defaultLanguage="javascript"
          options={{ overviewRulerLanes: 0, minimap: { enabled: false } }}
          defaultValue={defaultValue}
        />
      </Paper>
    </RootLayout>
  );
}
