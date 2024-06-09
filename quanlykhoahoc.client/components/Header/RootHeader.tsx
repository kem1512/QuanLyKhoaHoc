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
  Menu,
  UnstyledButton,
  useMantineTheme,
  ActionIcon,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import classes from "./RootHeader.module.css";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { useSelector } from "react-redux";
import Logo from "../Logo/Logo";
import {
  IconHeart,
  IconLogout,
  IconMessage,
  IconPlayerPause,
  IconSchoolBell,
  IconSettings,
  IconStar,
  IconSwitchHorizontal,
  IconTrash,
} from "@tabler/icons-react";
import ColorSchemeToggle from "../ColorSchemeToggle/ColorSchemeToggle";

const mockdata = [
  { label: "Trang Chủ", value: "/" },
  { label: "Khóa Học", value: "/course" },
  { label: "Blog", value: "/blog" },
  { label: "Quản Trị", value: "/dashboard" },
];

export function RootHeader() {
  const theme = useMantineTheme();

  const [drawerOpened, { toggle: toggleDrawer, close: closeDrawer }] =
    useDisclosure(false);
  const pathname = usePathname();
  const user = useSelector((state: any) => state.auth.user);

  return (
    <Box>
      <header style={{ height: rem("70px") }}>
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
                <Menu
                  width={260}
                  position="bottom-end"
                  transitionProps={{ transition: "pop-top-right" }}
                  withinPortal
                >
                  <Menu.Target>
                    <UnstyledButton>
                      <Group gap={7}>
                        <Avatar
                          src={user.avatar}
                          alt={user.name}
                          radius="xl"
                          size="md"
                        />
                        <Text fw={500} size="sm" lh={1} mr={3}>
                          {user.name}
                        </Text>
                      </Group>
                    </UnstyledButton>
                  </Menu.Target>
                  <Menu.Dropdown>
                    <Menu.Item
                      leftSection={
                        <IconHeart
                          style={{ width: rem(16), height: rem(16) }}
                          color={theme.colors.red[6]}
                          stroke={1.5}
                        />
                      }
                    >
                      Liked posts
                    </Menu.Item>
                    <Menu.Item
                      leftSection={
                        <IconStar
                          style={{ width: rem(16), height: rem(16) }}
                          color={theme.colors.yellow[6]}
                          stroke={1.5}
                        />
                      }
                    >
                      Saved posts
                    </Menu.Item>
                    <Menu.Item
                      leftSection={
                        <IconMessage
                          style={{ width: rem(16), height: rem(16) }}
                          color={theme.colors.blue[6]}
                          stroke={1.5}
                        />
                      }
                    >
                      Your comments
                    </Menu.Item>

                    <Menu.Label>Settings</Menu.Label>
                    <Link
                      href={"/profile/info"}
                      style={{ textDecoration: "none" }}
                    >
                      <Menu.Item
                        leftSection={
                          <IconSettings
                            style={{ width: rem(16), height: rem(16) }}
                            stroke={1.5}
                          />
                        }
                      >
                        Tài Khoản
                      </Menu.Item>
                    </Link>
                    <Menu.Item
                      leftSection={
                        <IconSwitchHorizontal
                          style={{ width: rem(16), height: rem(16) }}
                          stroke={1.5}
                        />
                      }
                    >
                      Change account
                    </Menu.Item>
                    <Menu.Item
                      leftSection={
                        <IconLogout
                          style={{ width: rem(16), height: rem(16) }}
                          stroke={1.5}
                        />
                      }
                    >
                      Logout
                    </Menu.Item>

                    <Menu.Divider />

                    <Menu.Label>Danger zone</Menu.Label>
                    <Menu.Item
                      leftSection={
                        <IconPlayerPause
                          style={{ width: rem(16), height: rem(16) }}
                          stroke={1.5}
                        />
                      }
                    >
                      Pause subscription
                    </Menu.Item>
                    <Menu.Item
                      color="red"
                      leftSection={
                        <IconTrash
                          style={{ width: rem(16), height: rem(16) }}
                          stroke={1.5}
                        />
                      }
                    >
                      Delete account
                    </Menu.Item>
                  </Menu.Dropdown>
                </Menu>
                <ColorSchemeToggle />
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
