"use client";

import RoleHandler from "../../../../components/Handler/RoleHandler";

export default function DashboardRoleUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <RoleHandler id={id} />;
}
