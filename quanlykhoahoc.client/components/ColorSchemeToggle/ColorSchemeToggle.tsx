'use client';

import { Switch, useMantineTheme, rem, useMantineColorScheme } from '@mantine/core';
import { IconSun, IconMoonStars } from '@tabler/icons-react';

export default function ColorSchemeToggle() {
  const theme = useMantineTheme();
  const { setColorScheme, colorScheme } = useMantineColorScheme();

  const sunIcon = (
    <IconSun
      style={{ width: rem(16), height: rem(16) }}
      stroke={2.5}
      color={theme.colors.yellow[4]}
    />
  );

  const moonIcon = (
    <IconMoonStars
      style={{ width: rem(16), height: rem(16) }}
      stroke={2.5}
      color={theme.colors.blue[6]}
    />
  );

  return (
    <Switch
      size="md"
      color="dark.4"
      onChange={() => setColorScheme(colorScheme === 'dark' ? 'light' : 'dark')}
      onLabel={sunIcon}
      offLabel={moonIcon}
      defaultChecked={colorScheme === 'dark'}
    />
  );
}
