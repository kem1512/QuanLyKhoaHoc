"use client";

import { AppShell, Container } from "@mantine/core";
import React, { Suspense } from "react";
import { RootHeader } from "../../Header/RootHeader";
import { RootFooter } from "../../Footer/Root/RootFooter";

export default function RootLayout({
  children,
  size,
}: {
  children: React.ReactNode;
  size?: string;
}) {
  return (
    <AppShell header={{ height: 70 }}>
      <AppShell.Header>
        <Container size={"xl"}>
          <RootHeader />
        </Container>
      </AppShell.Header>
      <AppShell.Main>
        <Container size={size ?? "xl"} p={"xs"}>
          <Suspense fallback={"Loading..."}>{children}</Suspense>
        </Container>
        <RootFooter />
      </AppShell.Main>
    </AppShell>
  );
}
