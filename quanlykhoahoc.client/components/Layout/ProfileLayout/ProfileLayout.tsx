"use client";

import { AppShell, Container, Grid } from "@mantine/core";
import React, { Suspense } from "react";
import Link from "next/link";
import classes from "./ProfileLayout.module.css";
import { usePathname } from "next/navigation";
import { RootHeader } from "../../Header/RootHeader";
import { RootFooter } from "../../Footer/Root/RootFooter";

const mockdata = [
  {
    label: "Thông Tin Cá Nhân",
    link: "/profile/info",
  },

  {
    label: "Đổi Mật Khẩu",
    link: "/profile/reset-password",
  },
  {
    label: "Khóa Học Của Tôi",
    link: "/profile/course",
  },
  {
    label: "Cài Đặt",
    link: "/profile/course",
  },
];

export default function ProfileLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const pathname = usePathname();

  return (
    <AppShell header={{ height: 70 }}>
      <AppShell.Header>
        <Container size={"xl"}>
          <RootHeader />
        </Container>
      </AppShell.Header>
      <AppShell.Main>
        <Container size="xl" p={"xs"}>
          <Suspense fallback={"Loading..."}>
            <Grid>
              <Grid.Col span={{ base: 12, lg: 2.5 }} className={classes.navbar}>
                <div className={classes.navbarMain}>
                  {mockdata.map((item) => {
                    return (
                      <Link
                        className={classes.link}
                        data-active={item.link === pathname ? "active" : null}
                        href={item.link}
                        key={item.label}
                      >
                        <span>{item.label}</span>
                      </Link>
                    );
                  })}
                </div>
              </Grid.Col>
              <Grid.Col span={{ base: 12, lg: 8.5 }}>{children}</Grid.Col>
            </Grid>
          </Suspense>
        </Container>
        <RootFooter />
      </AppShell.Main>
    </AppShell>
  );
}
