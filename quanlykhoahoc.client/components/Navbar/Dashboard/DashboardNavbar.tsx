import {
  Group,
  ScrollArea,
  rem,
  UnstyledButton,
  Avatar,
  Text,
  Box,
  ThemeIcon,
  Collapse,
} from "@mantine/core";
import {
  IconNotes,
  IconCalendarStats,
  IconGauge,
  IconChevronRight,
  IconAddressBook,
  IconUser,
} from "@tabler/icons-react";
import classes from "./DashboardNavbar.module.css";
import React, { useState } from "react";
import Link from "next/link";
import Logo from "../../Logo/Logo";
import { useSelector } from "react-redux";
import { UserMapping } from "../../../app/web-api-client";

const mockdata = [
  { label: "Quản Trị", icon: IconGauge, link: "/dashboard" },
  {
    label: "Người Dùng",
    icon: IconUser,
    link: "/dashboard/user",
  },
  {
    label: "Khóa Học",
    icon: IconNotes,
    initiallyOpened: true,
    links: [
      { label: "Chủ Đề", link: "/dashboard/subject" },
      { label: "Khóa Học", link: "/dashboard/course" },
    ],
  },
  {
    label: "Chứng Chỉ",
    icon: IconCalendarStats,
    links: [
      { label: "Loại Chứng Chỉ", link: "/dashboard/certificate-type" },
      { label: "Chứng Chỉ", link: "/dashboard/certificate" },
    ],
  },
  {
    label: "Vai Trò",
    icon: IconCalendarStats,
    link: "/dashboard/role",
  },
  {
    label: "Blog",
    icon: IconAddressBook,
    link: "/dashboard/blog",
  },
];

interface LinksGroupProps {
  icon: React.FC<any>;
  label: string;
  initiallyOpened?: boolean;
  links?: { label: string; link: string }[];
  link?: string;
}

export function LinksGroup({
  icon: Icon,
  label,
  initiallyOpened,
  links,
  link,
}: LinksGroupProps) {
  const hasLinks = Array.isArray(links);
  const [opened, setOpened] = useState(initiallyOpened || false);
  const items = (hasLinks ? links : []).map((link) => (
    <Link className={classes.link} href={link.link} key={link.label}>
      {link.label}
    </Link>
  ));

  return (
    <>
      {link ? (
        <Link
          href={link}
          className={classes.control}
          style={{ textDecoration: "none" }}
        >
          <Group justify="space-between" gap={0}>
            <Box style={{ display: "flex", alignItems: "center" }}>
              <ThemeIcon variant="light" size={30}>
                <Icon style={{ width: rem(18), height: rem(18) }} />
              </ThemeIcon>
              <Box ml="md">{label}</Box>
            </Box>
            {hasLinks && (
              <IconChevronRight
                className={classes.chevron}
                stroke={1.5}
                style={{
                  width: rem(16),
                  height: rem(16),
                  transform: opened ? "rotate(-90deg)" : "none",
                }}
              />
            )}
          </Group>
        </Link>
      ) : (
        <UnstyledButton
          onClick={() => {
            setOpened((o) => !o);
          }}
          className={classes.control}
        >
          <Group justify="space-between" gap={0}>
            <Box style={{ display: "flex", alignItems: "center" }}>
              <ThemeIcon variant="light" size={30}>
                <Icon style={{ width: rem(18), height: rem(18) }} />
              </ThemeIcon>
              <Box ml="md">{label}</Box>
            </Box>
            {hasLinks && (
              <IconChevronRight
                className={classes.chevron}
                stroke={1.5}
                style={{
                  width: rem(16),
                  height: rem(16),
                  transform: opened ? "rotate(-90deg)" : "none",
                }}
              />
            )}
          </Group>
        </UnstyledButton>
      )}
      {hasLinks ? <Collapse in={opened}>{items}</Collapse> : null}
    </>
  );
}

export function DashboardNavbar({ burger }: { burger: React.ReactNode }) {
  const user = useSelector((state: any) => state.auth.user) as UserMapping;

  const links = mockdata.map((item) => (
    <LinksGroup {...item} key={item.label} />
  ));

  return (
    <nav className={classes.navbar}>
      <div className={classes.header}>
        <Group justify="space-between">
          <Logo />
          {burger}
        </Group>
      </div>

      <ScrollArea className={classes.links}>
        <div className={classes.linksInner}>{links}</div>
      </ScrollArea>

      {user && (
        <div className={classes.footer}>
          <UnstyledButton className={classes.user}>
            <Group>
              <Avatar src={user.avatar} radius="xl" />

              <div style={{ flex: 1, width: 0 }}>
                <Text size="sm" fw={500} truncate>
                  {user.username}
                </Text>

                <Text c="dimmed" size="xs" className="email" truncate>
                  {user.email}
                </Text>
              </div>

              <IconChevronRight
                style={{ width: rem(14), height: rem(14) }}
                stroke={1.5}
              />
            </Group>
          </UnstyledButton>
        </div>
      )}
    </nav>
  );
}
