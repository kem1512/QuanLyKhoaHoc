"use client";

import {
  Group,
  Button,
  Text,
  Divider,
  Box,
  Burger,
  Drawer,
  ScrollArea,
  rem,
  Avatar,
  ActionIcon,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import classes from "./RootHeader.module.css";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { useSelector } from "react-redux";
import Logo from "../Logo/Logo";
import { IconNotification, IconSchoolBell } from "@tabler/icons-react";

const mockdata = [
  { label: "Trang Chủ", value: "/" },
  { label: "Khóa Học", value: "/course" },
  { label: "Blog", value: "/blog" },
  { label: "Quản Trị", value: "/dashboard" },
];

export function RootHeader() {
  const [drawerOpened, { toggle: toggleDrawer, close: closeDrawer }] =
    useDisclosure(false);
  const pathname = usePathname();
  const user = useSelector((state: any) => state.auth.user);

  return (
    <Box>
      <header className={classes.header}>
        <Group justify="space-between" h="100%">
          <Logo />

          <Group h="100%" gap={0} visibleFrom="sm">
            {mockdata.map((item) => {
              return (
                <Link
                  key={item.value}
                  href={item.value}
                  className={classes.link}
                  data-active={pathname === item.value || undefined}
                >
                  {item.label}
                </Link>
              );
            })}
          </Group>

          <Group visibleFrom="sm">
            {user ? (
              <Group>
                <ActionIcon variant="transparent">
                  <IconSchoolBell />
                </ActionIcon>
                <Avatar src={user.avatar} />
              </Group>
            ) : (
              <Link href={"/login"}>
                <Button size="xs">Đăng Nhập</Button>
              </Link>
            )}
          </Group>

          <Burger
            opened={drawerOpened}
            onClick={toggleDrawer}
            hiddenFrom="sm"
          />
        </Group>
      </header>

      <Drawer
        opened={drawerOpened}
        onClose={closeDrawer}
        size="100%"
        padding="md"
        title="Navigation"
        hiddenFrom="sm"
        zIndex={1000000}
      >
        <ScrollArea h={`calc(100vh - ${rem(80)})`} mx="-md">
          <Divider my="sm" />

          {mockdata.map((item) => {
            return (
              <Link key={item.value} href={item.value} className={classes.link}>
                {item.label}
              </Link>
            );
          })}

          <Divider my="sm" />

          <Group justify="center" grow pb="xl" px="md">
            <Button>Đăng Nhập</Button>
          </Group>
        </ScrollArea>
      </Drawer>
    </Box>
  );
}
