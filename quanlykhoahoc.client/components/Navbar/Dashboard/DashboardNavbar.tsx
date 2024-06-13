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
  IconUser,
  IconAperture,
  IconCertificate,
  IconArticle,
} from "@tabler/icons-react";
import classes from "./DashboardNavbar.module.css";
import React, { useState, useEffect } from "react";
import Link from "next/link";
import Logo from "../../Logo/Logo";
import { useSelector } from "react-redux";
import { UserMapping } from "../../../app/web-api-client";
import { usePathname } from "next/navigation";

const mockdata = [
  { label: "Quản Trị", icon: IconGauge, link: "/dashboard" },
  {
    label: "Người Dùng",
    icon: IconUser,
    links: [
      {
        label: "Người Dùng",
        icon: IconCalendarStats,
        link: "/dashboard/user",
      },
      {
        label: "Vai Trò",
        icon: IconCalendarStats,
        link: "/dashboard/role",
      },
    ],
  },
  {
    label: "Khóa Học",
    icon: IconNotes,
    initiallyOpened: true,
    links: [{ label: "Khóa Học", link: "/dashboard/course" }],
  },
  {
    label: "Chủ Đề",
    icon: IconAperture,
    links: [
      { label: "Chủ Đề", link: "/dashboard/subject" },
      { label: "Chi Tiết Chủ Đề", link: "/dashboard/subjectDetail" },
    ],
  },
  {
    label: "Chứng Chỉ",
    icon: IconCertificate,
    links: [
      { label: "Chứng Chỉ", link: "/dashboard/certificate" },
      { label: "Loại Chứng Chỉ", link: "/dashboard/certificate-type" },
    ],
  },
  {
    label: "Bài Tập",
    icon: IconCalendarStats,
    links: [
      {
        label: "Bài Tập",
        link: "/dashboard/practice",
      },
      {
        label: "Test Case",
        link: "/dashboard/test-case",
      },
      {
        label: "Ngôn Ngữ",
        link: "/dashboard/programing-language",
      },
    ],
  },
  {
    label: "Blog",
    icon: IconArticle,
    link: "/dashboard/blog",
  },
];

interface LinksGroupProps {
  icon: React.FC<any>;
  label: string;
  links?: { label: string; link: string }[];
  link?: string;
}

export function LinksGroup({
  icon: Icon,
  label,
  links,
  link,
}: LinksGroupProps) {
  const hasLinks = Array.isArray(links);
  const pathname = usePathname();
  const isCurrentPath = (path: string) => pathname === path;

  const [opened, setOpened] = useState(
    hasLinks && links.some((link) => isCurrentPath(link.link))
  );

  useEffect(() => {
    if (hasLinks) {
      setOpened(links.some((link) => isCurrentPath(link.link)));
    }
  }, [pathname, links]);

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
