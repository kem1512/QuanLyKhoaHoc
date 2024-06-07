"use client"

import { AppShell, Avatar, Burger, Group } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import React, { Suspense } from "react";
import { DashboardNavbar } from "../Navbar/Dashboard/DashboardNavbar";

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const [opened, { toggle }] = useDisclosure();

  return (
    <AppShell
      layout="alt"
      header={{ height: 60 }}
      footer={{ height: 60 }}
      navbar={{ width: 300, breakpoint: "sm", collapsed: { mobile: !opened } }}
      padding="md"
    >
      <AppShell.Header>
        <Group h="100%" px="md">
          <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
          <Avatar ms={"auto"} />
        </Group>
      </AppShell.Header>
      <AppShell.Navbar p="md">
        <DashboardNavbar />
      </AppShell.Navbar>
      <AppShell.Main>
        <Suspense>{children}</Suspense>
      </AppShell.Main>
      <AppShell.Footer p="md">Footer</AppShell.Footer>
    </AppShell>
  );
}
