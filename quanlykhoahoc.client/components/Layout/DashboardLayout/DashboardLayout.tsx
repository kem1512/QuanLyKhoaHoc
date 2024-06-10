"use client";

import { AppShell, Breadcrumbs, Burger, Group } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import React, { Suspense } from "react";
import { DashboardNavbar } from "../../Navbar/Dashboard/DashboardNavbar";
import { usePathname } from "next/navigation";
import Link from "next/link";
import ColorSchemeToggle from "../../ColorSchemeToggle/ColorSchemeToggle";

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const [opened, { toggle }] = useDisclosure();

  const pathname = usePathname();

  const pathSegments = pathname.split("/").slice(1);

  let accumulatedPath = "";

  return (
    <AppShell
      layout="alt"
      header={{ height: 60 }}
      navbar={{ width: 300, breakpoint: "sm", collapsed: { mobile: !opened } }}
      padding="md"
    >
      <AppShell.Header>
        <Group h="100%" px="md" justify="space-between">
          <Breadcrumbs>
            {pathSegments.map((segment, index) => {
              accumulatedPath += `/${segment}`;
              return (
                <Link
                  key={index}
                  href={accumulatedPath}
                  className="text-decoration-none"
                >
                  {segment}
                </Link>
              );
            })}
          </Breadcrumbs>
          <ColorSchemeToggle />
          <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
        </Group>
      </AppShell.Header>
      <AppShell.Navbar p="md" pb={0}>
        <DashboardNavbar
          burger={
            <Burger
              opened={opened}
              onClick={toggle}
              hiddenFrom="sm"
              size="sm"
            />
          }
        />
      </AppShell.Navbar>
      <AppShell.Main>
        <Suspense>{children}</Suspense>
      </AppShell.Main>
    </AppShell>
  );
}
