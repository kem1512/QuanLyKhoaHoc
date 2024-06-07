"use client";

import {
  Center,
  Group,
  Paper,
  RingProgress,
  SimpleGrid,
  Text,
  rem,
} from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout";
import { IconArrowUpRight, IconArrowDownRight } from "@tabler/icons-react";
import { BarChart, DonutChart } from "@mantine/charts";

const icons = {
  up: IconArrowUpRight,
  down: IconArrowDownRight,
};

const data = [
  {
    label: "Page views",
    stats: "456,578",
    progress: 65,
    color: "teal",
    icon: "up",
  },
  {
    label: "New users",
    stats: "2,550",
    progress: 72,
    color: "blue",
    icon: "up",
  },
  {
    label: "Orders",
    stats: "4,735",
    progress: 52,
    color: "red",
    icon: "down",
  },
];

export default function Dashboard() {
  const stats = data.map((stat) => {
    const Icon = icons[stat.icon];
    return (
      <Paper withBorder radius="md" p="xs" key={stat.label}>
        <Group>
          <RingProgress
            size={80}
            roundCaps
            thickness={8}
            sections={[{ value: stat.progress, color: stat.color }]}
            label={
              <Center>
                <Icon
                  style={{ width: rem(20), height: rem(20) }}
                  stroke={1.5}
                />
              </Center>
            }
          />

          <div>
            <Text c="dimmed" size="xs" tt="uppercase" fw={700}>
              {stat.label}
            </Text>
            <Text fw={700} size="xl">
              {stat.stats}
            </Text>
          </div>
        </Group>
      </Paper>
    );
  });

  return (
    <DashboardLayout>
      <Group gap={"xl"}>
        <SimpleGrid w={"100%"} cols={{ base: 1, sm: 3 }}>
          {stats}
        </SimpleGrid>
        <BarChart
          h={300}
          data={[
            { month: "January", Smartphones: 1200, Laptops: 900, Tablets: 200 },
            {
              month: "February",
              Smartphones: 1900,
              Laptops: 1200,
              Tablets: 400,
            },
            { month: "March", Smartphones: 400, Laptops: 1000, Tablets: 200 },
            { month: "April", Smartphones: 1000, Laptops: 200, Tablets: 800 },
            { month: "May", Smartphones: 800, Laptops: 1400, Tablets: 1200 },
            { month: "June", Smartphones: 750, Laptops: 600, Tablets: 1000 },
          ]}
          dataKey="month"
          series={[
            { name: "Smartphones", color: "violet.6" },
            { name: "Laptops", color: "blue.6" },
            { name: "Tablets", color: "teal.6" },
          ]}
          tickLine="y"
        />
        <DonutChart
          data={[
            { name: "USA", value: 400, color: "indigo.6" },
            { name: "India", value: 300, color: "yellow.6" },
            { name: "Japan", value: 100, color: "teal.6" },
            { name: "Other", value: 200, color: "gray.6" },
          ]}
        />
      </Group>
    </DashboardLayout>
  );
}
