"use client";

import { Grid, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  IRoleMapping,
  RoleClient,
  RoleCreate,
  RoleUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";

export default function RoleHandler({ id }: { id?: number }) {
  const RoleService = new RoleClient();

  const { data, mutate } = useSWR(`/api/role/${id}`, () => RoleService.getEntity(id));

  const [role, setRole] = useState<IRoleMapping>({
    roleName: "",
    roleCode: "",
  });

  useEffect(() => {
    if (data) setRole(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tên Vai Trò"
            placeholder="Nhập Tên Vai Trò"
            value={role.roleName}
            onChange={(e) =>
              setRole((prev) => ({ ...prev, roleName: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Mã Vai Trò"
            placeholder="Nhập Mã Vai Trò"
            value={role.roleCode}
            onChange={(e) =>
              setRole((prev) => ({ ...prev, roleCode: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return id
                  ? RoleService.updateEntity(role.id, role as RoleUpdate)
                  : RoleService.createEntity(role as RoleCreate);
              }, `${id ? "Sửa" : "Thêm"} Thành Công`, mutate)
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
